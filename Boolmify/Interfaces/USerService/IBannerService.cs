    using Boolmify.Dtos.BannerDto;

    namespace Boolmify.Interfaces.USerService;

    public interface IBannerService
    {
        Task<List<BannerDto>> GetAllBannersAsync();
    }