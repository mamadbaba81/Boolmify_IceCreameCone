    namespace Boolmify.Models;

    public class Courier
    {
        public int CourierId { get; set; }

        public string Name { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
        
        public List<Delivery> Deliveries { get; set; } = new();
    }