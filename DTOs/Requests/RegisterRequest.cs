using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public long? BranchId { get; set; }
        public long? RoleId { get; set; }
    }
}
