using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Code { get; set; }
        
        public string? PromotionCode { get; set; }

        public string? PromotionName { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal PromotionDiscountAmount { get; set; }

        public string? CouponCode { get; set; }

        public string? CouponName { get; set; }

        public string? CouponDiscountType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal CouponDiscountValue { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal BasisAmount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal FinalAmount { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal TaxAmount { get; set; }

        public string? Paymentstatus { get; set; }

        public string? PaymentMethod { get; set; }

        public DateTime PaidAt { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime ConfirmedAt { get; set; }

        public string? Notes { get; set; }

        public long CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User? Register { get; set; }

        public long? ConfirmedBy { get; set; } 
        [ForeignKey("ConfirmedBy")]
        public User? Staff { get; set; }

    }
}
