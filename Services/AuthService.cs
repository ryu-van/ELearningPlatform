using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_learning_platform.Services
{
    public class AuthService: IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (exists)
            {
                throw new Exception("Email đã tồn tại");
            }

            var user = new User
            {
                Code = Guid.NewGuid().ToString("N").Substring(0, 8),
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = HashPassword(request.Password),
                IsActive = false,
                BranchId = request.BranchId,
                RoleId = request.RoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var verification = CreateEmailVerificationToken(user.Id);
            _context.EmailVerificationTokens.Add(verification);
            await _context.SaveChangesAsync();

            var roleName = await _context.Roles.Where(r => r.Id == user.RoleId).Select(r => r.Name).FirstOrDefaultAsync() ?? string.Empty;
            await _emailService.SendActivationEmailAsync(user.Email, verification.Token);
            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                RoleName = roleName,
                AccessToken = string.Empty,
                RefreshToken = string.Empty
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                throw new Exception("Tài khoản không tồn tại");
            }
            if (!user.IsActive)
            {
                throw new Exception("Tài khoản chưa được kích hoạt");
            }
            if (!VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new Exception("Mật khẩu không đúng");
            }

            var roleName = user.Role?.Name ?? string.Empty;
            var refreshToken = CreateRefreshToken(user.Id);
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                RoleName = roleName,
                AccessToken = GenerateJwtToken(user, roleName),
                RefreshToken = refreshToken.Token!
            };
        }

        public async Task<AuthResponse> RefreshAsync(RefreshRequest request)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == request.RefreshToken);
            if (token == null || token.RevokedAt != null || token.ExpiresAt <= DateTime.UtcNow)
            {
                throw new Exception("Refresh token không hợp lệ hoặc đã hết hạn");
            }
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) throw new Exception("Tài khoản không tồn tại");
            if (!user.IsActive) throw new Exception("Tài khoản chưa được kích hoạt");

            token.RevokedAt = DateTime.UtcNow;
            var newRefresh = CreateRefreshToken(user.Id);
            token.ReplacedByToken = newRefresh.Token;
            _context.RefreshTokens.Add(newRefresh);
            await _context.SaveChangesAsync();

            var roleName = user.Role?.Name ?? string.Empty;
            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                RoleName = roleName,
                AccessToken = GenerateJwtToken(user, roleName),
                RefreshToken = newRefresh.Token!
            };
        }

        private string HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[16];
            rng.GetBytes(salt);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(32);
            return $"v1${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash)) return false;
            var parts = passwordHash.Split('$');
            if (parts.Length == 3 && parts[0] == "v1")
            {
                var salt = Convert.FromBase64String(parts[1]);
                var expected = Convert.FromBase64String(parts[2]);
                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
                var actual = pbkdf2.GetBytes(32);
                return CryptographicOperations.FixedTimeEquals(expected, actual);
            }
            using var sha256 = SHA256.Create();
            var legacy = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return legacy == passwordHash;
        }

        private string GenerateJwtToken(User user, string roleName)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var issuer = jwtSection["Issuer"] ?? string.Empty;
            var audience = jwtSection["Audience"] ?? string.Empty;
            var secret = jwtSection["Secret"] ?? string.Empty;
            var minutes = int.TryParse(jwtSection["AccessTokenMinutes"], out var m) ? m : 60;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.FullName),
                new Claim(ClaimTypes.Role, roleName)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken CreateRefreshToken(long userId)
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                ExpiresAt = DateTime.UtcNow.AddDays(int.TryParse(_configuration["Jwt:RefreshTokenDays"], out var d) ? d : 7),
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
        }

        private EmailVerificationToken CreateEmailVerificationToken(long userId)
        {
            return new EmailVerificationToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                CreatedAt = DateTime.UtcNow
            };
        }

        public async Task<bool> VerifyEmailAsync(VerifyEmailRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) return false;
            var token = await _context.EmailVerificationTokens.FirstOrDefaultAsync(t => t.Token == request.Token && t.UserId == user.Id);
            if (token == null || token.UsedAt != null || token.ExpiresAt <= DateTime.UtcNow) return false;
            token.UsedAt = DateTime.UtcNow;
            user.IsEmailVerified = true;
            user.IsActive = true;
            user.EmailVerifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
