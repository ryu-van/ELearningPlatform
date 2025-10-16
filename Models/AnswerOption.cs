using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("AnswerOptions")]
    public class AnswerOption:BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Content { get; set; }

        public bool IsCorrect { get; set; }

        public int OrderNo { get; set; }

        public string? Explanation { get; set; }

        public long QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }


    }
}
