using System.ComponentModel.DataAnnotations;

namespace E_learning_platform.DTOs.Requests
{
    public class CreateOrderRequest
    {
        [Required]
        public long CourseId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? CouponCode { get; set; }
        public string? PromotionCode { get; set; }
    }
}
