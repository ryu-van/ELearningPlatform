using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class SubmitAnswerRequest
    {
        [Required]
        public long AttemptId { get; set; }

        [Required]
        public long QuestionId { get; set; }

        public long? OptionId { get; set; }

        public string? AnswerText { get; set; }
    }
}
