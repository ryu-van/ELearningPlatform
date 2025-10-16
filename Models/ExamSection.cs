using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("ExamSections")]
    public class ExamSection : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Instructions { get; set; }

        public int OrderNo { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public Decimal TotalMarks { get; set; }

        public long ExamId { get; set; }
        [ForeignKey("ExamId")]
        public Exam? Exam { get; set; }
    }
}
