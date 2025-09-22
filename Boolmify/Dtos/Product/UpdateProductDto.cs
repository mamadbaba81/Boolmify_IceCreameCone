    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Product;

    public class UpdateProductDto
    {
        public int?  CategoryId { get; set; }
        public string? ProductName { get; set; } 
        public string? Slug { get; set; }
        public string? Sku { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        [Range(0.0, double.MaxValue)]
        public decimal? Price { get; set; }
        [Range(0.0, double.MaxValue)]
        public decimal?  DiscountPrice { get; set; }
        [Range(0, int.MaxValue)]
        public int? StockQuantity { get; set; }
        
        public bool? IsActive { get; set; }
        
        public List<int>? AddOnIds { get; set; }
        
        public List<int>? OccasionIds { get; set; } 
    }