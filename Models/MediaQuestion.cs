using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("MediaQuestions")]
    public class MediaQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? MediaType { get; set; }

        public string? MediaUrl { get; set; }

        public string? MediaCaption { get; set; }

        public int? Duration { get; set; }

        public long FileSize { get; set; }


        public long? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }

    }
}
