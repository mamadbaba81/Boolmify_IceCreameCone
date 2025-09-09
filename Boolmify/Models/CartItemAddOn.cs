    namespace Boolmify.Models;

    public class CartItemAddOn
    {
        public int  CartItemId { get; set; }

        public CartItem   CartItem { get; set; } =  default!;

        public int  ProductAddonId { get; set; }

        public ProductAddOn  ProductAddOn { get; set; } = default!;
        
        
    }