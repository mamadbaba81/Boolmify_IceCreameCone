    using Boolmify.Dtos.Product;
    using Boolmify.Helper;
    using Boolmify.Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    namespace Boolmify.Interfaces.USerService;

    public interface IProductCatalogService
    {
        Task<QueryObject<Product>> GetAllAsync( 
            string? search = null,
                int pageNumber = 1,
                int pageSize = 10,
                string? sortBy = null,
                bool isAscending = true);
        Task<Product> GetByIdAsync(int id);
        
    }