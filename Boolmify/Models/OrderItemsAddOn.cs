    namespace Boolmify.Models;

    public class OrderItemsAddOn
    {
        public int OrderItemId { get; set;}

        public OrderItem OrderItem { get; set; } = default!;

        public int  ProductAddOnId { get; set; }
        
        public ProductAddOn ProductAddOn { get; set; } =  default!;
        
    }