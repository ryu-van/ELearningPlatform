namespace E_learning_platform.DTOs.Responses
{
    public class LanguageResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public bool IsActive { get; set; }
    }
}
