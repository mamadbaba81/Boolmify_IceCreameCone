    namespace Boolmify.Models;

    public class Delivery
    {
        public int  DeliveryId { get; set; }

        public int  OrderId { get; set; }

        public Order  Order { get; set; } = default!;

        public int  CourierId { get; set; }

        public Courier  Courier { get; set; } =  default!;

        public DateTime  AssignedAt { get; set; } = DateTime.Now;
        
        public DateTime? DeliveredAt { get; set; }

        public string?  ProofImageUrl { get; set; }

        public DeliveryStatus  DliveryStatus { get; set; } = DeliveryStatus.Pending;
        
        public enum DeliveryStatus
        {
            Pending,
            InProgress,
            Delivered,
            Failed
        }
        
    }