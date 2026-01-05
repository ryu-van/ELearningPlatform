using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
