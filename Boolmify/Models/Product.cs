    namespace Boolmify.Models;

    public class Product
    {
        public int  ProductId { get; set; }
        
        public int  CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        
        public string ProductName { get; set; }

        public string? Slug { get; set; }

        public string? Sku { get; set; }
        
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
        
        public decimal Price { get; set; }

        public decimal?  DiscountPrice { get; set; }
        
        public int StockQuantity { get; set; } = 0;
        
        public bool IsActive { get; set; }
        
        public virtual List<Comment> Comments { get; set; } = new ();
        
        public virtual List<Review> Reviews { get; set; } = new();
        
        public virtual List<ProductOccasion> ProductOccasions { get; set; } = new(); 
        
        public virtual List<ProductAddOn> AddOns { get; set; } = new(); 
    }
    