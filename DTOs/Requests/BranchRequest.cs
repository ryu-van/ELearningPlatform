using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class BranchRequest
    {
        public string? Code { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
