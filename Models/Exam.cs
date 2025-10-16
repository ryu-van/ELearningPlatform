using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Exams")]
    public class Exam:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int Duration { get; set; }

        public string? Instructions { get; set; }

        public string? Type { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public Decimal PassingScore { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal TotalMarks { get; set; }

        public string? Status { get; set; }

        public bool IsRandomized { get; set; } = false;

        public bool ShowResults { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public int MaxAtttempts { get; set; } 

        public long? ParentExamId { get; set; }
        [ForeignKey("ParentExamId")]
        public Exam? ParentExam { get; set; }



    }
}
