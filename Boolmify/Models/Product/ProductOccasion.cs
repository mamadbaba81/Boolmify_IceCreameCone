    namespace Boolmify.Models;

    public class ProductOccasion
    {
        public int ProductId { get; set; }
        
        public virtual Product Product { get; set; } = default!;

        public int OccasionId { get; set; }
        
        public virtual Occasion Occasion { get; set; } = default!;
    }