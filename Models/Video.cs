using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Videos")]
    public class Video:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Type { get; set; }

        public string? Url { get; set; }

        public int? Duration { get; set; }

        public long? FileSize { get; set; }

        public string? Resolution { get; set; }

        public string? ThumbnailUrl { get; set; }

        public int ViewCount { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public long LessonId { get; set; }
        [ForeignKey("LessonId")]
        public Lesson? Lesson { get; set; }

        public long LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language? Language { get; set; }

        public long LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level? Level { get; set; }


    }
}
