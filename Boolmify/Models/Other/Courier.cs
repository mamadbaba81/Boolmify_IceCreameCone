    namespace Boolmify.Models;

    public class Courier
    {
        public int CourierId { get; set; }

        public string Name { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
        
        public  virtual List<Delivery> Deliveries { get; set; } = new();

        public bool  Isactive { get; set; } =  true;
        
    }