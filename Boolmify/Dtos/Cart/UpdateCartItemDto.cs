    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Cart;

    public class UpdateCartItemDto
    {
        [Required]
        public int CartItemId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public List<int>? AddOnIds { get; set; }
    }