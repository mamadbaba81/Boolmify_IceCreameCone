    using Boolmify.Data;
    using Boolmify.Dtos.FAQ;
    using Boolmify.Interfaces.ADminRepository;

    namespace Boolmify.Repository.AdminRepository;

    public class AdminFAQRepository:IAdminFAQService
    {
        
        private readonly ApplicationDBContext _Context;

        public AdminFAQRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<FAQDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FAQDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FAQDto> CreateAsync(CreateFAQDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<FAQDto> UpdateAsync(UpdateFAQDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }