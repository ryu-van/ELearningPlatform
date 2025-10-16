namespace E_learning_platform.DTOs.Responses
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public string RoleName { get; set; }

    }
}
