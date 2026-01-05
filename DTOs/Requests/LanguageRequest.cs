using System.ComponentModel.DataAnnotations;
namespace E_learning_platform.DTOs.Requests
{
    public class LanguageRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
