using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class GradeEssayRequest
    {
        [Required]
        public long AttemptId { get; set; }

        [Required]
        public long QuestionId { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal Score { get; set; }

        public string? Feedback { get; set; }
    }
}
