using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class ReviewRequest
    {
        [Required]
        public long CourseId { get; set; }

        [Required]
        [Range(1, 5)]
        public decimal Rating { get; set; }

        public string? Content { get; set; }
    }
}
