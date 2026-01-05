using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class VideoRequest
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? Type { get; set; }

        public string? Url { get; set; }
        
        public IFormFile? VideoFile { get; set; }

        public int? Duration { get; set; }

        public long? FileSize { get; set; }

        public string? Resolution { get; set; }

        public string? ThumbnailUrl { get; set; }
        
        public IFormFile? ThumbnailFile { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public long LessonId { get; set; }

        [Required]
        public long LanguageId { get; set; }

        [Required]
        public long LevelId { get; set; }
    }
}
