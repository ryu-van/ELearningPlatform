using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("CourseExams")]
    public class CourseExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int OrderNo { get; set; }

        public bool IsRequired { get; set; } = true;

        public DateTime AvailableFrom { get; set; }

        public DateTime AvailableUntil { get; set; }

        public long CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        public long ExamId { get; set; }
        [ForeignKey("ExamId")]
        public Exam? Exam { get; set; }



    }
}
