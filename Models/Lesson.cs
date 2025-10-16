using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Lessons")]
    public class Lesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? Type { get; set; }

        public int DurationMinutes { get; set; }

     
        public int OrderNo { get; set; }

        public long ChapterId { get; set; }

        public bool isActive { get; set; } = true;

        [ForeignKey("ChapterId")]
        public Chapter? Chapter { get; set; }

    }
}
