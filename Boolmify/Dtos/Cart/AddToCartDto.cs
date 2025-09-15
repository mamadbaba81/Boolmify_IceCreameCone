    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Cart;

    public class AddToCartDto
    {
        [Required]
        public int  ProductId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; } = 1;

        public List<int>?  AddOnIds { get; set; }
    }