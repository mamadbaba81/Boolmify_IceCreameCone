    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Models;

    public class Product
    {
        public int  ProductId { get; set; }
        
        public int  CategoryId { get; set; }
        
        public virtual Category Category { get; set; } = default!;
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = default!;
        [MaxLength(200)]
        public string? Slug { get; set; }
        [MaxLength(50)]
        public string? Sku { get; set; }
        
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, double.MaxValue)]
        public decimal?  DiscountPrice { get; set; }
        
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; } = 0;
        
        public bool IsActive { get; set; } = true;
        
        public virtual List<Comment> Comments { get; set; } = new ();
        
        public virtual List<Review> Reviews { get; set; } = new();
        
        public virtual List<ProductOccasion> ProductOccasions { get; set; } = new(); 
        
        public virtual List<ProductAddOnMap> AddOns { get; set; } = new(); 
    }
    