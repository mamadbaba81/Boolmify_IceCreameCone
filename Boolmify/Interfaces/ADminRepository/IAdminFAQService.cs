    using Boolmify.Dtos.FAQ;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminFAQService
    {
        Task<IEnumerable<FAQDto>>  GetAllAsync(string? search = null, int pageNumber = 1, int pageSize = 10);
        
        Task<FAQDto?> GetByIdAsync(int id);
        
        Task<FAQDto> CreateAsync(CreateFAQDto dto);
        
        Task<FAQDto> UpdateAsync(UpdateFAQDto dto);
        
        Task<FAQDto> ToogleActiveAsync (int id);
        Task<bool> DeleteAsync (int id);
        
        
    }