using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{

    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }

        public string? AvatarUrl { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsEmailVerified { get; set; } = false;

        public DateTime EmailVerifiedAt { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime LastLoginAt { get; set; }

        public string City { get; set; } = string.Empty;

        public string province { get; set; } = string.Empty;


        public long? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public ICollection<Post> posts { get; set; } = new List<Post>();

        public long? BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch? Branch { get; set; }


    }
}
