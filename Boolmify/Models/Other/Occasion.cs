    namespace Boolmify.Models;

    public class Occasion
    {
        public int OccasionId { get; set; }

        public string Name { get; set; } = default!;
        
        public string? Description { get; set; }

        public string? Slug { get; set; }
        
        public string? IconUrl { get; set; } // مثلا عکس کیک تولد یا قلب

        public bool  IsActive { get; set; } =  true;

        public DateTime  CreateAt { get; set; } = DateTime.Now;

        // ریلیشن
        public virtual List<ProductOccasion> ProductOccasions { get; set; } = new();
        
        
    }