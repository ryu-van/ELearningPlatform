using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("VocabularyCards")]
    public class VocabularyCard : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // === Từ vựng chính ===
        [Required]
        [MaxLength(255)]
        public string Term { get; set; } = string.Empty;

        // === Nghĩa của từ ===
        [Required]
        public string Definition { get; set; } = string.Empty;

        // === Ví dụ sử dụng ===
        public string? Example { get; set; }

        // === Phiên âm quốc tế (IPA) ===
        [MaxLength(100)]
        public string? IPA { get; set; }

        // === Đường dẫn âm thanh phát âm ===
        [MaxLength(500)]
        public string? AudioUrl { get; set; }

        // === Ảnh minh họa từ ===
        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        // === Loại từ (danh từ, động từ, tính từ, ...) ===
        [MaxLength(50)]
        public string? PartOfSpeech { get; set; }

        // === Từ đồng nghĩa / trái nghĩa ===
        [MaxLength(500)]
        public string? Synonyms { get; set; }

        [MaxLength(500)]
        public string? Antonyms { get; set; }

        // === Mức độ khó ===
        [MaxLength(20)]
        public string? DifficultyLevel { get; set; } // Beginner / Intermediate / Advanced

        // === Mức độ phổ biến ===
        [MaxLength(20)]
        public string? UsageFrequency { get; set; } // Common / Uncommon / Rare

        // === Cờ hoạt động ===
        public bool IsActive { get; set; } = true;
    }
}
