
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace E_learning_platform.Models
{
    [Table("SystemSettings")]
    public class SystemSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string? Key { get; set; }

        public string? Value { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }



    }
}
