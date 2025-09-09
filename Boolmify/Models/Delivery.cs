    namespace Boolmify.Models;

    public class Delivery
    {
        public int  DeliveryId { get; set; }

        public int  OrderId { get; set; }

        public int  CourierId { get; set; }

        public DateTime  AssignedAt { get; set; } = DateTime.Now;

        public string  ProofImageUrl { get; set; }
        
        
    }