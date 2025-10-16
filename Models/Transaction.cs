using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_learning_platform.Models
{
    [Table("Transactions")]
    public class Transaction : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? Gateway { get; set; }

        public string? GatewayTransId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public string? Status { get; set; }

        public string? BankCode { get; set; }

        public string? PaymentMethod { get; set; }

        public string? ResponseCode { get; set; }

        public string? ResponseMessage { get; set; }

        public string? RawResponse { get; set; }

        public string? IpAddress { get; set; }

        public long OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }







    }
}
