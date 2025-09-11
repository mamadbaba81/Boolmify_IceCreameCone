    namespace Boolmify.Models;

    public class Cart
    {
        public int  CartId { get; set; }

        public int  USerId { get; set; }
        
        public virtual AppUser User { get; set; } = default!;

        public DateTime  CreatedAt { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; } = true;
        
        public virtual List<CartItem> Items { get; set; } = new();
        
    }