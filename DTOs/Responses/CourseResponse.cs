using E_learning_platform.Models;

namespace E_learning_platform.DTOs.Responses
{
    public class CourseResponse
    {
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? Type { get; set; }

        public string? Description { get; set; }

        public int Capacity { get; set; }

        public int CurrentEnrollment { get; set; }

        public string? ShortDescription { get; set; }

        public string? ThumbnailUrl { get; set; }

        public decimal Price { get; set; }

        public string? Currency { get; set; }

        public decimal Duration { get; set; }

        public string? Status { get; set; }

        public bool IsActive { get; set; }

        public bool isFeatured { get; set; }

        public decimal Rating { get; set; }

        public int ViewCount { get; set; }

        public DateTime EnrollmentStartDate { get; set; }

        public DateTime EnrollmentEndDate { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }

        public long TeacherId { get; set; }
        public UserResponse? Teacher { get; set; }

        public long LanguageId { get; set; }

        public long LevelId { get; set; }

        public long BranchId { get; set; }
        public BranchResponse? Branch { get; set; }
    }
}
