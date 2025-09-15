    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Order;

    public class CreateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public List<int>? AddOnIds { get; set; }
    }