    namespace Boolmify.Models;

    public class ProductOccasion
    {
        public int ProductId { get; set; }
        
        public Product Product { get; set; } = default!;

        public int OccasionId { get; set; }
        
        public Occasion Occasion { get; set; } = default!;
    }