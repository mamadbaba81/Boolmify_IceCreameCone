    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Boolmify.Models;

    public class CartItem
    {
        public int  CartItemId { get; set; }
        
        public int  CartId { get; set; }
        
        public virtual Cart Cart { get; set; } = default!;
        
        public int ProductId { get; set; }
        
        public virtual Product Product { get; set; } = default!;
        
        
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        [NotMapped]
        public decimal TotalPrice => Quantity * UnitPrice;

        public virtual List<CartItemAddOn> CartItemAddOns { get; set; } = new();

    }