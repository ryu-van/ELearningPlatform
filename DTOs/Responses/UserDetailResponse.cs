namespace E_learning_platform.DTOs.Responses
{
    public class UserDetailResponse
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;  // ← THÊM DÒNG NÀY

        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
       
    }
}