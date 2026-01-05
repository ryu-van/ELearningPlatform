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

        [Required]
        public long AttemptId { get; set; }
        [ForeignKey(nameof(AttemptId))]
        public ExamAttempt? Attempt { get; set; }

        [Required]
        public long QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public Question? Question { get; set; }

        public long? OptionId { get; set; }
        [ForeignKey(nameof(OptionId))]
        public AnswerOption? Option { get; set; }

        public string? AnswerText { get; set; }

        public bool? IsCorrect { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public decimal? Score { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public DateTime? GradedAt { get; set; }

        public long? GradedBy { get; set; }
        [ForeignKey(nameof(GradedBy))]
        public User? Grader { get; set; }
    }
}
