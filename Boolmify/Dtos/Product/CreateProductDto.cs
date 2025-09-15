    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Product;

    public class CreateProductDto
    {
        [Required]
        public int  CategoryId { get; set; }
        [Required]
        [StringLength(150)]
        public string ProductName { get; set; } = default!;
        
        public string? Slug { get; set; }
        [StringLength(50)]
        public string? Sku { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        
        public string? ImageUrl { get; set; }
        
        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }
        
        [Range(0.0, double.MaxValue)]
        public decimal?  DiscountPrice { get; set; }
        
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; } = 0;

        public bool IsActive { get; set; } = true;
        
        public List<int> AddOnIds { get; set; } = new();
        
        public List<int> OccasionIds { get; set; } = new();
    }