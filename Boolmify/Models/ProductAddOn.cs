    namespace Boolmify.Models;

    public class ProductAddOn
    {
        public int ProductAddOnId { get; set; }
        
        public string ProductAddOnsName { get; set; }
        
        public decimal Price { get; set; }

        public List<OrderItemsAddOn>  OrderItemsAddOns { get; set; }  = new();

        public List<CartItemAddOn> CartItemAddOns { get; set; } = new();



    }