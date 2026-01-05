using E_learning_platform.Data;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;
namespace E_learning_platform.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResponse<User>> GetPagedUsersAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var query = _context.Users.AsNoTracking().Include(u => u.Role).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(u => EF.Functions.Like(u.FullName, $"%{keyword}%") || EF.Functions.Like(u.Email, $"%{keyword}%"));
            }
            if (isActive.HasValue)
            {
                query = query.Where(u => u.IsActive == isActive.Value);
            }
            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<User>(items, totalItems, page, pageSize);
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Users.AsNoTracking().Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> ChangeStatusAsync(long id, bool status)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            user.IsActive = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
