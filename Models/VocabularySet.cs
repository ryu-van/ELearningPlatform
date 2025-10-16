using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("VocabularySets")]
    public class VocabularySet : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // === Mã duy nhất cho bộ từ vựng ===
        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Category { get; set; }

        [MaxLength(500)]
        public string? ThumbnailUrl { get; set; }

        public bool IsPublic { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public int CardCount { get; set; } = 0;

        // === Ngôn ngữ ===
        public long? LanguageId { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }

        // === Cấp độ (Level) ===
        public long? LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public Level? Level { get; set; }

        // === Người tạo ===
        public long? CreatedBy { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        public User? Creator { get; set; }
    }
}
