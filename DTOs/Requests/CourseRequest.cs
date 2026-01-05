using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class CourseRequest
    {
        [Required]
        public string Title { get; set; }

        public string? Type { get; set; }

        public string? Description { get; set; }

        public int Capacity { get; set; }

        public string? ShortDescription { get; set; }

        public string? ThumbnailUrl { get; set; }
        
        public IFormFile? ThumbnailFile { get; set; }

        public decimal Price { get; set; }

        public string? Currency { get; set; }

        public decimal Duration { get; set; }

        public string? Status { get; set; }

        public bool IsActive { get; set; } = true;

        public bool isFeatured { get; set; } = false;

        public DateTime EnrollmentStartDate { get; set; }

        public DateTime EnrollmentEndDate { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }

        [Required]
        public long TeacherId { get; set; }

        [Required]
        public long LanguageId { get; set; }

        [Required]
        public long LevelId { get; set; }

        [Required]
        public long BranchId { get; set; }
    }
}
