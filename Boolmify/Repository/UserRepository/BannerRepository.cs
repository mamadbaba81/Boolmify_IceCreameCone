    using Boolmify.Data;
    using Boolmify.Dtos.BannerDto;
    using Boolmify.Interfaces.USerService;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class BannerRepository: IBannerService
    {
        private readonly ApplicationDBContext  _Context;

        public BannerRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        public async Task<List<BannerDto>> GetAllBannersAsync()
        {
            var banner = await _Context.Banners.Where(b => b.IsActive).OrderBy(b => b.CreatedAt).ToListAsync();
            return banner.Select(b=>new BannerDto
            {
                BannerId = b.BannerId,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                LinkUrl = b.LinkUrl,
                IsActive = b.IsActive,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            }).ToList();
        }
    }