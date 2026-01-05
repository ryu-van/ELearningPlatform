namespace E_learning_platform.DTOs.Responses
{
    public class ExamAttemptResponse
    {
        public long Id { get; set; }
        public int AttemptNumber { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public int TimeSpentSeconds { get; set; }
        public decimal TotalScore { get; set; }
        public decimal MaxScore { get; set; }
        public decimal PercentageScore { get; set; }
        public bool IsPassed { get; set; }
        public string Status { get; set; } = "InProgress";
        public long ExamId { get; set; }
        public string ExamTitle { get; set; } = string.Empty;
    }
}
