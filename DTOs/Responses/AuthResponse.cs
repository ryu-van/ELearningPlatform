namespace E_learning_platform.DTOs.Responses
{
    public class AuthResponse
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
