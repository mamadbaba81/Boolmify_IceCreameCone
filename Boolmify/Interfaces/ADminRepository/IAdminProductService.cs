    using Boolmify.Dtos.FAQ;
    using Boolmify.Dtos.Product;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(string? search, int pageNumber, int pageSize);
        
        Task<ProductDto?> GetByIdAsync(int id);
        
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        
        Task<ProductDto> UpdateAsync(int id , UpdateProductDto dto);
        
        Task<bool> DeleteAsync(int id);
        
        Task<bool> ToggleActiveAsync(int id ,  bool isActive);
        
        Task<bool> SetStockAsync(int id, int newquantity);
        
        Task<bool> AddjustStockAsync(int id, int addquantity);
        
        Task<bool> UpdatePriceAsync(int id, Decimal newprice , decimal? discountprice = null);
        
        
        
    }