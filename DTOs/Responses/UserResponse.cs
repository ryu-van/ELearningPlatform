namespace E_learning_platform.DTOs.Responses
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string RoleName { get; set; } = string.Empty;

    }
}
