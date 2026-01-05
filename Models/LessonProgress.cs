using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("LessonProgress")]
    public class LessonProgress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime CompletedAt { get; set; }

        public int TimeSpentInSeconds { get; set; } = 0;

        public DateTime LastAccessedAt { get; set; } 

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public long LessonId { get; set; }
        [ForeignKey("LessonId")]
        
        public Lesson? Lesson { get; set; }

        public LessonProgress()
        {
            
        }
    }
}
