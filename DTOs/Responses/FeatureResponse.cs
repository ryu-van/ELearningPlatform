namespace E_learning_platform.DTOs.Responses
{
    public class FeatureResponse
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsEnabled { get; set; }
    }
}
