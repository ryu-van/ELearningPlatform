namespace E_learning_platform.Dto.Requests
{
    public class RoleRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;


    }
}
