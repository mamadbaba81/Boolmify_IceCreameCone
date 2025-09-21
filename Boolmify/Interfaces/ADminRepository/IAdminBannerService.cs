    using Boolmify.Dtos.BannerDto;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminBannerService
    {
        Task<IEnumerable<BannerDto>> GetAllBannersAsync();
        
        Task<BannerDto?> GetByIdBannerAsync(int id);
        
        Task<BannerDto> CreateBannerAsync(CreateBannerDto Dto);
        
        Task<BannerDto?> UpdateBannerAsync(UpdateBannerDto  Dto);

        Task<bool> toggleActiveBannerAsync(int id);
        Task<bool> DeleteBannerAsync(int id);
    }