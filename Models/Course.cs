using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Courses")]
    public class Course: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Title { get; set; }

        public string?  Type { get; set; }

        public string? Description { get; set; }


        public int Capacity { get; set; }

        public int CurrentEnrollment { get; set; } = 0;

        public string? ShortDescription { get; set; }

        public string? ThumbnailUrl { get; set; }



        public decimal Price { get; set; }

        public string? Currency { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public Decimal Duration { get; set;}

        public string? Status { get; set; }

        public bool IsActive { get; set; } = true;

        public bool isFeatured { get; set; } = false;

        [Column(TypeName = "decimal(3,2)")]
        public Decimal Rating { get; set; } = 0;

        public int ViewCount { get; set; } = 0;

        public DateTime EnrollmentStartDate { get; set; }

        public DateTime EnrollmentEndDate { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }


        public long TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public User? Teacher { get; set; }

        public long LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language? Language { get; set; }

        public long LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level? Level { get; set; }

        public long BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch? Branch { get; set; }




    }
}
