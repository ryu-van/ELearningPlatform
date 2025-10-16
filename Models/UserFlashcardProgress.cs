using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("UserFlashcardProgress")]
    public class UserFlashcardProgress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // === Liên kết ===
        [Required]
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        public long CardId { get; set; }
        [ForeignKey(nameof(CardId))]
        public VocabularyCard? Card { get; set; }

        // === Tiến trình học ===
        public bool IsKnown { get; set; } = false;

        [MaxLength(20)]
        public string? Confidence { get; set; } // Again / Hard / Good / Easy

        public DateTime LastReviewed { get; set; } = DateTime.UtcNow;

        public int ReviewCount { get; set; } = 0;

        public int CorrectCount { get; set; } = 0;

        public int IncorrectCount { get; set; } = 0;

        public DateTime? NextReview { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        [Range(1.3, 5.0)]
        public decimal EaseFactor { get; set; } = 2.5m; // SM-2 default

        [Range(0, int.MaxValue)]
        public int IntervalDays { get; set; } = 1;

        [Range(0, int.MaxValue)]
        public int Repetitions { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
