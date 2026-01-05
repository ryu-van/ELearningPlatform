namespace E_learning_platform.DTOs.Responses
{
    public class ExamResponse
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public string? Instructions { get; set; }
        public string? Type { get; set; }
        public decimal PassingScore { get; set; }
        public decimal TotalMarks { get; set; }
        public string? Status { get; set; }
        public bool IsRandomized { get; set; }
        public bool ShowResults { get; set; }
        public bool IsActive { get; set; }
        public int MaxAtttempts { get; set; }
        public long? ParentExamId { get; set; }
    }
}
