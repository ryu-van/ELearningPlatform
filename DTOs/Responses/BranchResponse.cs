namespace E_learning_platform.DTOs.Responses
{
    public class BranchResponse
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; }


    }
}
