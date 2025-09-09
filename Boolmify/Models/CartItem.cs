    namespace Boolmify.Models;

    public class CartItem
    {
        public int  CartItemId { get; set; }
        
        public int  CartId { get; set; }
        
        public virtual Cart Cart { get; set; } = default!;
        
        public int ProductId { get; set; }
        
        public virtual Product Product { get; set; } = default!;
        
        public int Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalPrice => Quantity * UnitPrice;
        
    }