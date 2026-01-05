using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class ChapterRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public int OrderNo { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public long CourseId { get; set; }
    }
}
