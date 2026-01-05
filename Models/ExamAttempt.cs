using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("ExamAttempts")]
    public class ExamAttempt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AttemptNumber { get; set; } = 1;

        [Required]
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime? SubmittedAt { get; set; }

        public int TimeSpentSeconds { get; set; } = 0;

        [Column(TypeName = "decimal(8,2)")]
        public decimal TotalScore { get; set; } = 0;

        [Column(TypeName = "decimal(8,2)")]
        [Required]
        public decimal MaxScore { get; set; } = 0;

        [Column("Percentage", TypeName = "decimal(5,2)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal PercentageScore { get; private set; } 

        public bool IsPassed { get; set; } = false;

        [MaxLength(20)]
        public string Status { get; set; } = "InProgress"; 

        [Required]
        public long ExamId { get; set; }

        [ForeignKey(nameof(ExamId))]
        public Exam? Exam { get; set; }

        [Required]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
