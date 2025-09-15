    namespace Boolmify.Models;

    public class CartItemAddOn
    {
        public int  CartItemId { get; set; }

        public virtual CartItem   CartItem { get; set; } =  default!;

        public int  ProductAddOnId { get; set; }

        public virtual ProductAddOn  ProductAddOn { get; set; } = default!;
        
        
    }