namespace E_learning_platform.DTOs.Responses
{
    public class VideoResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string Url { get; set; }
        public int? Duration { get; set; }
        public long? FileSize { get; set; }
        public string? Resolution { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public long LessonId { get; set; }
        public long LanguageId { get; set; }
        public long LevelId { get; set; }
    }
}
