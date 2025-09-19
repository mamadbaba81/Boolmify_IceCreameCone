    using Boolmify.Data;
    using Boolmify.Dtos.AddOn;
    using Boolmify.Dtos.CommentsDtos;
    using Boolmify.Dtos.Product;
    using Boolmify.Dtos.Review;
    using Boolmify.Interfaces.ADminRepository;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.AdminRepository;

    public class AdminProductRepository:IAdminProductService
    {
        
        private readonly ApplicationDBContext _Context;

        public AdminProductRepository(ApplicationDBContext context)
        {
            _Context=context;
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync(string? search, int pageNumber, int pageSize)
        {
            var query = _Context.Products
                .Include(p => p.Comments).ThenInclude(c => c.User)
                .Include(p => p.Comments).ThenInclude(c => c.Replies)
                .Include(p => p.Reviews)
                .Include(p => p.ProductOccasions).ThenInclude(po => po.Occasion)
                .Include(p => p.AddOns).ThenInclude(pa => pa.ProductAddOn)
                .AsQueryable();
            if (!string.IsNullOrEmpty(search)) 
                query = query.Where(q=>q.ProductName.Contains(search));
            var product = await query.OrderByDescending(q => q.CreatedAt).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Description = p.Description,
                    StockQuantity = p.StockQuantity,
                    CreatedAt = p.CreatedAt,
                    CategoryId = p.CategoryId,
                    Sku = p.Sku,
                    Slug = p.Slug,
                    ImageUrl = p.ImageUrl,
                    DiscountPrice = p.DiscountPrice,
                    IsActive = p.IsActive,
                    Comments = p.Comments.Select(c => new CommentDto
                    {
                        CommentId = c.CommentId,
                        ProductId = c.ProductId,
                        UserName = c.User.UserName,
                        Content = c.Content,
                        CreatedAt = c.CreatedAt,
                        ParentCommentId = c.ParentCommentId,
                        Replies = c.Replies.Select(r => new CommentDto
                        {
                            CommentId = c.CommentId,
                            ProductId = c.ProductId,
                            UserName = c.User.UserName,
                            Content = c.Content,
                            CreatedAt = c.CreatedAt,
                            ParentCommentId = c.ParentCommentId,
                        }).ToList(),

                    }).ToList(),
                            Reviews = p.Reviews.Select(r=>new ReviewDto
                            {
                                ReviewId = r.ReviewId,
                                Rating = r.Rating,
                                Comment = r.Comment
                            }).ToList(),
                            ProductOccasions = p.ProductOccasions.Select(po=>new ProductOccasionDto
                            {
                                OccasionId = po.OccasionId,
                                Name = po.Occasion.Name,
                                IconUrl = po.Occasion.IconUrl,
                                
                            }).ToList(),
                            
                            AddOns = p.AddOns.Select(a=>new ProductAddonDto
                            {
                                ProductAddOnId = a.ProductAddOnId,
                                Price = a.ProductAddOn.Price,
                                ProductAddOnsName = a.ProductAddOn.ProductAddOnsName
                                
                                
                            }).ToList(),


                }).ToListAsync();
            
            return product;
        }

        public async Task<ProductDto?> GetAsync(int id)
        {
            var product = await _Context.Products
                .Include(p => p.Comments)
                .Include(p => p.Reviews)
                .Include(p => p.ProductOccasions)
                .ThenInclude(po => po.Occasion)
                .Include(p => p.AddOns)
                .ThenInclude(pa => pa.ProductAddOn)
                .FirstOrDefaultAsync();
            if(product==null)return null;
            return new ProductDto
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                CreatedAt = product.CreatedAt,
                CategoryId = product.CategoryId,
                Sku = product.Sku,
                Slug = product.Slug,
                ImageUrl = product.ImageUrl,
                DiscountPrice = product.DiscountPrice,
                IsActive = product.IsActive,

                Comments = product.Comments.Select(c => new CommentDto
                {
                    CommentId = c.Id,
                    ProductId = c.ProductId,
                    UserName = c.User.UserName,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    ParentCommentId = c.ParentCommentId,
                    Replies = c.Replies.Select(r => new CommentDto
                    {
                        CommentId = r.Id,
                        ProductId = r.ProductId,
                        UserName = r.User.UserName,
                        Content = r.Content,
                        CreatedAt = r.CreatedAt,
                        ParentCommentId = r.ParentCommentId,
                    }).ToList()
                }).ToList(),

                Reviews = product.Reviews.Select(r => new ReviewDto
                {
                    ReviewId = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment
                }).ToList(),

                ProductOccasions = product.ProductOccasions.Select(po => new ProductOccasionDto
                {
                    OccasionId = po.OccasionId,
                    Name = po.Occasion.Name,
                    IconUrl = po.Occasion.IconUrl
                }).ToList(),

                AddOns = product.AddOns.Select(a => new ProductAddonDto
                {
                    AddOnId = a.ProductAddOnId,
                    AddOnName = a.ProductAddOn.ProductAddOnsName,
                    Price = a.ProductAddOn.Price
                }).ToList(),

            };
        }

        public Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> UpdateAsync(UpdateProductDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ToggleActiveAsync(int id, bool isActive)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStockAsync(int id, int newquantity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePriceAsync(int id, decimal newprice, decimal? discountprice = null)
        {
            throw new NotImplementedException();
        }
    }