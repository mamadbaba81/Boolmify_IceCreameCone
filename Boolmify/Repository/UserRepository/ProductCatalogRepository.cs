    using Boolmify.Data;
    using Boolmify.Dtos.Product;
    using Boolmify.Helper;
    using Boolmify.Interfaces.USerService;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class ProductCatalogRepository : IProductCatalogService
    {
        private readonly ApplicationDBContext _context;

        public ProductCatalogRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<QueryObject<Product>> GetAllAsync(string? search = null, int pageNumber = 1,
            int pageSize = 10, string? sortBy = null,
            bool isAscending = true)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Category);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.ProductName.Contains(search) || p.Description.Contains(search));
            }

            query = sortBy?.ToLower() switch
            {
                "price" => isAscending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
                "name" => isAscending ? query.OrderBy(p => p.ProductName) : query.OrderByDescending(p => p.ProductName),
                _ => query.OrderBy(p => p.ProductId)
            };

            var totalCount = await query.CountAsync();

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new QueryObject<Product>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }
    }