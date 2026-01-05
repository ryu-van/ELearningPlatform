using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("TranscriptPractices")]
    public class TranscriptPractice: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        public long VideoId { get; set; }
        [ForeignKey(nameof(VideoId))]
        public Video? Video { get; set; }

        public long? LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public Level? Level { get; set; }

        public long? LanguageId { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }

        public string? Content { get; set; }

        public string? ReferenceContent { get; set; }

        [MaxLength(100)]
        [Required]
        public string Type { get; set; } = "Dictation";

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)]
        public decimal? Score { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)]
        public decimal? Accuracy { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)]
        public decimal? Fluency { get; set; }

        public int? DurationSeconds { get; set; }

        public string? FeedbackText { get; set; }
    }
}
