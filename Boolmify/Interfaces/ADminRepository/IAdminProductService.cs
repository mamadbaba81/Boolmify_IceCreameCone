    using Boolmify.Dtos.FAQ;
    using Boolmify.Dtos.Product;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(string? search, int pageNumber, int pageSize);
        Task<ProductDto?> GetAsync(int id);
        
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        
        Task<ProductDto> UpdateAsync(UpdateProductDto dto);
        
        Task<bool> DeleteAsync(int id);
        
        Task<bool> ToggleActiveAsync(int id ,  bool isActive);
        
        Task<bool> UpdateStockAsync(int id, int newquantity);
        
        Task<bool> UpdatePriceAsync(int id, Decimal newprice , decimal? discountprice = null);
        
        
        
    }