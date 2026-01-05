namespace E_learning_platform.DTOs.Responses
{
    public class OrderResponse
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public long CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public decimal BasisAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string? Paymentstatus { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime PaidAt { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime ConfirmedAt { get; set; }
    }
}
