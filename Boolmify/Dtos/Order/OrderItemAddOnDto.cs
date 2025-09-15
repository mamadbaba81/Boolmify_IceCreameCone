    namespace Boolmify.Dtos.Order;

    public class OrderItemAddOnDto
    {
        public int ProductAddOnId { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }

    }