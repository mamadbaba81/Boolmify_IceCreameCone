    using Boolmify.Data;
    using Boolmify.Dtos.AddOn;
    using Boolmify.Dtos.CommentsDtos;
    using Boolmify.Dtos.Product;
    using Boolmify.Dtos.Review;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
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

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _Context.Products
                .Include(p => p.Comments).ThenInclude(comment => comment.User).Include(product => product.Comments)
                .ThenInclude(comment => comment.Replies).ThenInclude(comment => comment.User)
                .Include(p => p.Reviews)
                .Include(p => p.ProductOccasions)
                .ThenInclude(po => po.Occasion)
                .Include(p => p.AddOns).ThenInclude(pa => pa.ProductAddOn)
                .FirstOrDefaultAsync();
            if(product==null)return null;
            return new ProductDto
            {
                ProductId = product.ProductId,
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
                    CommentId = c.CommentId,
                    ProductId = c.ProductId,
                    UserName = c.User.UserName,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    ParentCommentId = c.ParentCommentId,
                    Replies = c.Replies.Select(r => new CommentDto
                    {
                        CommentId = r.CommentId,
                        ProductId = r.ProductId,
                        UserName = r.User.UserName,
                        Content = r.Content,
                        CreatedAt = r.CreatedAt,
                        ParentCommentId = r.ParentCommentId,
                    }).ToList()
                }).ToList(),

                Reviews = product.Reviews.Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
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
                    ProductAddOnId = a.ProductAddOnId,
                    ProductAddOnsName = a.ProductAddOn.ProductAddOnsName,
                    Price = a.ProductAddOn.Price
                }).ToList(),

            };
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CreatedAt = DateTime.UtcNow,
                CategoryId = dto.CategoryId,
                Sku = dto.Sku,
                Slug = dto.Slug,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                DiscountPrice = dto.DiscountPrice,
                IsActive = dto.IsActive,

            };
            _Context.Products.Add(product);
            await _Context.SaveChangesAsync();
            if (dto.OccasionIds != null && dto.OccasionIds.Any())
            {
                foreach (var occId in dto.OccasionIds)
                {
                    _Context.ProductOccasions.Add(new ProductOccasion
                    {
                        OccasionId = occId,
                        ProductId = product.ProductId,
                    });
                }
            }

            if (dto.AddOnIds != null && dto.AddOnIds.Any())
            {
                foreach (var addOnId in dto.AddOnIds)
                {
                    _Context.ProductAddOnMaps.Add(new ProductAddOnMap
                    {
                        ProductId = product.ProductId,
                        ProductAddOnId = addOnId
                    });
                }
            }
            await _Context.SaveChangesAsync();
            
            return await GetByIdAsync(product.ProductId) ?? throw new Exception("Product creation failed");
        }

        public async Task<ProductDto> UpdateAsync(int id ,UpdateProductDto dto)
        {
            var product = await _Context.Products.Include(p=>p.ProductOccasions)
                .Include(p=>p.AddOns).FirstOrDefaultAsync(p=>p.ProductId == id);
            if (product == null) return null;
            
            if (dto.CategoryId.HasValue)  product.CategoryId = dto.CategoryId.Value;
            if (!string.IsNullOrWhiteSpace(dto.Sku))  product.Sku = dto.Sku;
            if (!string.IsNullOrWhiteSpace(dto.Slug))  product.Slug = dto.Slug;
            if (!string.IsNullOrWhiteSpace(dto.Description))  product.Description = dto.Description;
            if (!string.IsNullOrWhiteSpace(dto.ImageUrl))  product.ImageUrl = dto.ImageUrl;
            if (!string.IsNullOrWhiteSpace(dto.ProductName)) product.ProductName = dto.ProductName;
            if (dto.DiscountPrice.HasValue) product.DiscountPrice = dto.DiscountPrice.Value;
            if (dto.IsActive.HasValue) product.IsActive = dto.IsActive.Value;
            if (dto.Price.HasValue) product.Price = dto.Price.Value;
            if (dto.StockQuantity.HasValue) product.StockQuantity = dto.StockQuantity.Value;

            if (dto.OccasionIds != null)
            {
                _Context.ProductOccasions.RemoveRange(product.ProductOccasions);
                foreach (var occid in dto.OccasionIds)
                {
                    _Context.ProductOccasions.Add(new ProductOccasion
                    {
                        ProductId = product.ProductId,
                        OccasionId = occid
                    });
                }
                
            }

            if (dto.AddOnIds != null)
            {
                _Context.ProductAddOnMaps.RemoveRange(product.AddOns);
                foreach (var addOnId in dto.AddOnIds)
                {
                    _Context.ProductAddOnMaps.Add(new ProductAddOnMap
                    {
                        ProductId = product.ProductId,
                        ProductAddOnId = addOnId
                    });
                    
                }
            }
            await _Context.SaveChangesAsync();
            return await GetByIdAsync(product.ProductId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _Context.Products.Include(p => p.ProductOccasions).Include(p => p.AddOns)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null) return false;
            _Context.ProductOccasions.RemoveRange(product.ProductOccasions);
            _Context.ProductAddOnMaps.RemoveRange(product.AddOns);
            _Context.Products.Remove(product);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(int id, bool isActive)
        {
            var product = await _Context.Products.FindAsync(id);
            if (product == null) return false;
            product.IsActive = !product.IsActive;
            await _Context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> AddjustStockAsync(int id, int addquantity)
        {
            var  product = await _Context.Products.FindAsync(id);
            if (product == null) return false;
            product.StockQuantity += addquantity;
            if (product.StockQuantity < 0) product.StockQuantity = 0;
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetStockAsync(int id, int newquantity)
        {
            var  product = await _Context.Products.FindAsync(id);
            if (product == null) return false;
            product.StockQuantity = newquantity < 0 ?  0 : newquantity;
            await _Context.SaveChangesAsync();
            return true;
        }
        

        public async Task<bool> UpdatePriceAsync(int id, decimal newprice, decimal? discountprice = null)
        {
           var  product = await _Context.Products.FindAsync(id);
           if (product == null) return false;
           product.Price = newprice;
           if (discountprice.HasValue) product.DiscountPrice = discountprice.Value;
           else  product.DiscountPrice = null;
           await _Context.SaveChangesAsync();
           return true;
        }
    }