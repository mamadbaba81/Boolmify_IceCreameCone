    using Boolmify.Dtos.Coupon;
    using Boolmify.Dtos.Payment;

    namespace Boolmify.Dtos.Order;

    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public string RecipientName { get; set; } = default!;
        public string RecipientPhone { get; set; } = default!;
        public string RecipientAddress { get; set; } = default!;

        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public DateTime CreateAt { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();
        public List<CouponDto> Coupons { get; set; } = new();
        public List<PaymentDto> Payments { get; set; } = new();
    }