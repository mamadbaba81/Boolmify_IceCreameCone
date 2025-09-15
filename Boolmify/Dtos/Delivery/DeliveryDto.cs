    namespace Boolmify.Dtos.Delivery;

    public class DeliveryDto
    {
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }

        public int CourierId { get; set; }
        public string CourierName { get; set; } = default!;
        public string CourierPhone { get; set; } = default!;

        public DateTime AssignedAt { get; set; }
        public string? ProofImageUrl { get; set; }
        
    }