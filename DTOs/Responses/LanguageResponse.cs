namespace E_learning_platform.DTOs.Responses
{
    public class LanguageResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; }
    }
}
