namespace E_learning_platform.DTOs.Responses
{
    public class QuestionResponse
    {
        public long Id { get; set; }
        public string? Type { get; set; }
        public string? Content { get; set; }
        public string? Explanation { get; set; }
        public decimal Points { get; set; }
        public int OrderNo { get; set; }
        public string? DifficultyLevel { get; set; }
        public bool IsActive { get; set; }
        public long SectionId { get; set; }
    }
}
