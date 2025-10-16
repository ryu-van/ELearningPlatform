using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("UserAnswers")]
    public class UserAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // 🔗 Liên kết với lần làm bài (ExamAttempt)
        [Required]
        public long AttemptId { get; set; }
        [ForeignKey(nameof(AttemptId))]
        public ExamAttempt? Attempt { get; set; }

        // 🔗 Liên kết với câu hỏi
        [Required]
        public long QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public Question? Question { get; set; }

        // 🔗 Liên kết với lựa chọn (nếu là câu trắc nghiệm)
        public long? OptionId { get; set; }
        [ForeignKey(nameof(OptionId))]
        public AnswerOption? Option { get; set; }

        // 📝 Câu trả lời dạng text (tự luận, điền từ, v.v.)
        public string? AnswerText { get; set; }

        // ✅ Đúng / Sai (nullable vì có thể chưa chấm)
        public bool? IsCorrect { get; set; }

        // 🧮 Điểm cho câu này
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public decimal? Score { get; set; }

        // 🕐 Thời gian nộp bài
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        // 🧾 Thời gian chấm
        public DateTime? GradedAt { get; set; }

        // 👤 Người chấm (nếu có)
        public long? GradedBy { get; set; }
        [ForeignKey(nameof(GradedBy))]
        public User? Grader { get; set; }
    }
}
