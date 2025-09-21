    using Boolmify.Data;
    using Boolmify.Dtos.BannerDto;
    using Boolmify.Interfaces.ADminRepository;

    namespace Boolmify.Repository.AdminRepository;

    public class AdminBannerRepository:IAdminBannerService
    {
        
        private readonly ApplicationDBContext  _Context;

        public AdminBannerRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<BannerDto>> GetAllBannersAsync()
        {
          
        }

        public async Task<BannerDto?> GetByIdBannerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BannerDto> CreateBannerAsync(CreateBannerDto Dto)
        {
            throw new NotImplementedException();
        }

        public async Task<BannerDto?> UpdateBannerAsync(UpdateBannerDto Dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteBannerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> toggleActiveBannerAsync(int id)
        {
            throw new NotImplementedException();
        }
    }