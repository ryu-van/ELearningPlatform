using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        public string? Type { get; set; }

        public string? Content { get; set; }

        public string? Explanation { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public Decimal Points { get; set; }

        public int OrderNo { get; set; }

        public string? DifficultyLevel { get; set; }

        public bool IsActive { get; set; } = true;

        public long SectionId { get; set; }
        [ForeignKey("SectionId")]
        public ExamSection? ExamSection { get; set; }

    }
}
