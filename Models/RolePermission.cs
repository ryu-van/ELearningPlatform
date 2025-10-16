using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("RolePermissions")]
    public class RolePermission
    {

        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        public long FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public Feature? Feature { get; set; }


        public bool IsEnabled { get; set; } = false;


    }
}
