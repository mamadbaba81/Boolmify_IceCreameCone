    using Boolmify.Dtos.AddOn;
    using Boolmify.Dtos.CommentsDtos;
    using Boolmify.Dtos.Review;
    using Boolmify.Models;

    namespace Boolmify.Dtos.Product;

    public class ProductDto
    {
        public int  ProductId { get; set; }
        public int  CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string ProductName { get; set; } = default!;
        public string? Slug { get; set; }
        public string? Sku { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal?  DiscountPrice { get; set; }
        public int StockQuantity { get; set; } = 0;
        public bool IsActive { get; set; }
        public List<CommentDto> Comments { get; set; } = new ();
        public List<ReviewDto> Reviews { get; set; } = new();
        public  List<ProductOccasionDto> ProductOccasions { get; set; } = new(); 
        public List<ProductAddonDto> AddOns { get; set; } = new();
        public DateTime  CreatedAt { get; set; } 
    }