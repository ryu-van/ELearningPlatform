using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class ExamRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int Duration { get; set; }

        public string? Instructions { get; set; }

        public string? Type { get; set; }

        [Range(0, 100)]
        public decimal PassingScore { get; set; }

        public decimal TotalMarks { get; set; }

        public string? Status { get; set; }

        public bool IsRandomized { get; set; } = false;

        public bool ShowResults { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public int MaxAtttempts { get; set; }

        public long? ParentExamId { get; set; }
    }
}
