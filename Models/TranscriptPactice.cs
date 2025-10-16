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

        // === Người học ===
        [Required]
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        // === Video luyện nghe / nói ===
        [Required]
        public long VideoId { get; set; }
        [ForeignKey(nameof(VideoId))]
        public Video? Video { get; set; }

        // === Cấp độ bài học ===
        public long? LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public Level? Level { get; set; }

        // === Ngôn ngữ ===
        public long? LanguageId { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }

        // === Nội dung học viên nhập vào ===
        public string? Content { get; set; }

        // === Bản transcript gốc của video ===
        public string? ReferenceContent { get; set; }

        // === Loại bài luyện (Dictation / Shadowing / Comprehension / Pronunciation) ===
        [MaxLength(100)]
        [Required]
        public string Type { get; set; } = "Dictation";

        // === Kết quả điểm tổng hợp ===
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)]
        public decimal? Score { get; set; }

        // === Độ chính xác (Accuracy %) ===
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)]
        public decimal? Accuracy { get; set; }

        // === Độ lưu loát (Fluency %) ===
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)]
        public decimal? Fluency { get; set; }

        // === Thời lượng luyện tập (giây) ===
        public int? DurationSeconds { get; set; }

        // === Nhận xét hệ thống hoặc giáo viên ===
        public string? FeedbackText { get; set; }
    }
}
