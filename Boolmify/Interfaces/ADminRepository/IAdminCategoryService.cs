    using Boolmify.Dtos.Category;
    using Boolmify.Dtos.FAQ;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminCategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetAsync(int id);
        
        Task<CategoryDto>  CreateAsync(CreateCategoryDto dto);
        
        Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto);
        
        Task<bool> DeleteAsync(int id);
        
        
    }