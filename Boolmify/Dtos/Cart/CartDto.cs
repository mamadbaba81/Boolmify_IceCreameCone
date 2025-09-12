    using Boolmify.Models;

    namespace Boolmify.Dtos.Cart;

    public class CartDto
    {
        public int  CartId { get; set; }

        public int  UserId { get; set; }
        
        public DateTime  CreateAt { get; set; }

        public bool  IsActive { get; set; }

        public List<CartItemDto> Items { get; set; } = new();

        public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
    }