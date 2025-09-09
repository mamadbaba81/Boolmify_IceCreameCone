    namespace Boolmify.Models;

    public class Occasion
    {
        public int OccasionId { get; set; }

        public string Name { get; set; } = default!;
        
        public string? Description { get; set; }
        
        public string? IconUrl { get; set; } // مثلا عکس کیک تولد یا قلب

        // ریلیشن
        public virtual List<ProductOccasion> ProductOccasions { get; set; } = new();
        
    }