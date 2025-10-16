namespace E_learning_platform.DTOs.Responses
{
    public class UserDetailResponse
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }  // ← THÊM DÒNG NÀY

        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
       
    }
}