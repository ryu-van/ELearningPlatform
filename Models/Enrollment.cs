using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Enrollments")]
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime ErrolledAt { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string? Status { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Progress { get; set; } = 0;

        public DateTime? CompletedAt { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public DateTime? LastAccessedAt { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("CourseId")]
        public long CourseId { get; set; }
        public Course? Course { get; set; }



    }
}
