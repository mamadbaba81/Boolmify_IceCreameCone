    using Boolmify.Dtos.FAQ;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminFAQService
    {
        Task<IEnumerable<FAQDto>>  GetAllAsync();
        
        Task<FAQDto?> GetByIdAsync(int id);
        
        Task<FAQDto> CreateAsync(CreateFAQDto dto);
        
        Task<FAQDto> UpdateAsync(UpdateFAQDto dto);
        
        Task<bool> DeleteAsync(int id);
        
        
    }