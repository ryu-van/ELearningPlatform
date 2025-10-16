namespace E_learning_platform.DTOs.Responses
{
    public class RoleResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public List<FeatureResponse> Features { get; set; } = new();


    }
}
