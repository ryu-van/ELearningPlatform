using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Posts")]
    public class Post : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Slug { get; set; }

        public string? Content { get; set; }

        private string? Excerpt { get; set; }

        public int ViewCount { get; set; } = 0;


        public bool IsPublished { get; set; }

        public DateTime PublishedAt { get; set; }


        public String? ThumnailUrl { get; set; }

        public long AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public User? Author { get; set; }


    }
}
