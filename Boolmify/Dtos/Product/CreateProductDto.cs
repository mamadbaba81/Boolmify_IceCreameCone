    namespace Boolmify.Dtos.Product;

    public class CreateProductDto
    {
        public int  CategoryId { get; set; }

        public string ProductName { get; set; } = default;
        
        public string? Slug { get; set; }
        
        public string? Sku { get; set; }
        
        public string? Description { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal?  DiscountPrice { get; set; }
        
        public int StockQuantity { get; set; } = 0;
        
        public bool IsActive { get; set; }
        
        public List<int> AddOnIds { get; set; } = new();
        
        public List<int> OccasionIds { get; set; } = new();
    }