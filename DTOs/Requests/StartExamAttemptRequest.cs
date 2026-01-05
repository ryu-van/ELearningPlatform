using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class StartExamAttemptRequest
    {
        [Required]
        public long ExamId { get; set; }
    }
}
