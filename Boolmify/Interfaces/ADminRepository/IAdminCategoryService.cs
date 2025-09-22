    using Boolmify.Dtos.Category;
    using Boolmify.Dtos.FAQ;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminCategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync(string? search = null, int pageNumber = 1, int pageSize = 10);
        Task<CategoryDto?> GetByIdAsync(int id);
        
        Task<CategoryDto>  CreateAsync(CreateCategoryDto dto);
        
        Task<CategoryDto> UpdateAsync(int id ,UpdateCategoryDto dto);
        
        Task<bool> DeleteAsync(int id);
        
        Task<IEnumerable<CategoryDto>> GetTreeAsync();
        
        
    }