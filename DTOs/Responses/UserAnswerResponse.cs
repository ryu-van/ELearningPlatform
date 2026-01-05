namespace E_learning_platform.DTOs.Responses
{
    public class UserAnswerResponse
    {
        public long Id { get; set; }
        public long AttemptId { get; set; }
        public long QuestionId { get; set; }
        public long? OptionId { get; set; }
        public string? AnswerText { get; set; }
        public bool? IsCorrect { get; set; }
        public decimal? Score { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
