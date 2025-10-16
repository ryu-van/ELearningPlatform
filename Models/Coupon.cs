using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Coupons")]
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Code { get; set; } = null!;

        public string? Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? DiscountType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal DiscountValue { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public Decimal MinOrderAmount { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public Decimal MaxDiscountAmount { get; set; }

        public int UsageLimit { get; set; } = 1;

        public int UsedCount { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime StartDate { get; set; }

        public DateTime ExpireDate { get; set; }

    }
}
