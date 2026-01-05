using E_learning_platform.Models;

namespace E_learning_platform.DTOs.Responses
{
    public class EnrollmentResponse
    {
        public long Id { get; set; }
        public DateTime ErrolledAt { get; set; }
        public string? Status { get; set; }
        public decimal? Progress { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? LastAccessedAt { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public long CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
    }
}
