using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Promotions")]
    public class Promotion : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal DiscountValue { get; set; }

        public bool IsActive { get; set; } = true;


        public DateTime StartAt { get; set; } = DateTime.UtcNow;

        public DateTime ExpireAt { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
