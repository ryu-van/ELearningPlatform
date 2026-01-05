namespace E_learning_platform.DTOs.Responses
{
    public class ReviewResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? UserAvatar { get; set; }
        public long CourseId { get; set; }
        public decimal Rating { get; set; }
        public string? Content { get; set; }
        public bool IsVerifiedPurchase { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
