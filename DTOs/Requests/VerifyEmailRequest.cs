using System.ComponentModel.DataAnnotations;
namespace E_learning_platform.DTOs.Requests
{
    public class VerifyEmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;
    }
}
