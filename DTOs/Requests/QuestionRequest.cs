using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class QuestionRequest
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Content { get; set; }

        public string? Explanation { get; set; }

        public decimal Points { get; set; }

        public int OrderNo { get; set; }

        public string? DifficultyLevel { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public long SectionId { get; set; }
    }
}
