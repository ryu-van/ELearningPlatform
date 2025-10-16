using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Features")]
    public class Feature : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;


        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    }
}
