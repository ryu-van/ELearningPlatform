using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("CourseReviews")]
    public class CourseReview : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        public long CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; }

        [Required]
        [Column(TypeName = "decimal(2,1)")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public decimal Rating { get; set; }

        public string? Content { get; set; }

        public bool IsVerifiedPurchase { get; set; } = false;

        public bool IsPublished { get; set; } = true;

    }
}
