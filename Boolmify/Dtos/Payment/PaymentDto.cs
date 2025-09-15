    namespace Boolmify.Dtos.Payment;

    public class PaymentDto
    {
        public int  PaymentId { get; set; }
        public int  OrderId { get; set; }

        public decimal  Amount { get; set; }
        public string Method { get; set; } = default!;
        public string  Status { get; set; } =  default!;

        public DateTime  PaidAt { get; set; }
    }