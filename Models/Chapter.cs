using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Chapters")]
    public class Chapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Title { get; set; }

        public int OrderNo { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public long CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }


    }
}
