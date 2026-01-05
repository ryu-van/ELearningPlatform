namespace E_learning_platform.DTOs.Responses
{
    public class ChapterResponse
    {
        public long Id { get; set; }

        public string? Title { get; set; }

        public int OrderNo { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public long CourseId { get; set; }
    }
}
