using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace E_learning_platform.Models
{
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string? Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ReplacedByToken { get; set; }

        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        public bool IsActive => RevokedAt == null && ExpiresAt > DateTime.UtcNow;


      
    }
}
