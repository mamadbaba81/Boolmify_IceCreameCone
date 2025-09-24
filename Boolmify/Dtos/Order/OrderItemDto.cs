    namespace Boolmify.Dtos.Order;

    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string ShippingAddress { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
        public string Status { get; set; } = "Pending";
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        public List<OrderItemAddOnDto> AddOns { get; set; } = new();
    }