namespace E_learning_platform.DTOs.Responses
{
    public class LevelResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}
