namespace E_learning_platform.DTOs.Responses
{
    public class LessonResponse
    {
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? Type { get; set; }

        public int DurationMinutes { get; set; }

        public int OrderNo { get; set; }

        public bool isActive { get; set; }

        public long ChapterId { get; set; }
    }
}
