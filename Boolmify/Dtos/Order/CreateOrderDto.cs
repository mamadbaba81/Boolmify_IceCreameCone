    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Order;

    public class CreateOrderDto
    {
        [Required]
        public string RecipientName { get; set; } = default!;

        [Required]
        [Phone]
        public string RecipientPhone { get; set; } = default!;

        [Required]
        public string RecipientAddress { get; set; } = default!;

        [Required]
        public DateTime DeliveryDate { get; set; }

        public string? CouponCode { get; set; }

        [Required]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }