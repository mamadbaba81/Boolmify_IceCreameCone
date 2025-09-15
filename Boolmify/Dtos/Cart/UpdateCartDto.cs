    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Cart;

    public class UpdateCartDto
    {
        [Required]
        public int  CartId { get; set; }

        public bool?  IsActive { get; set; }
    }