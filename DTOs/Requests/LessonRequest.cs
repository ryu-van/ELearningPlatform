using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class LessonRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public string? Type { get; set; }

        public int DurationMinutes { get; set; }

        public int OrderNo { get; set; }

        public bool isActive { get; set; } = true;

        [Required]
        public long ChapterId { get; set; }
    }
}
