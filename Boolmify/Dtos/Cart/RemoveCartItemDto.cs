    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Cart;

    public class RemoveCartItemDto
    {
        [Required]
        public int  CartItemId { get; set; }
    }