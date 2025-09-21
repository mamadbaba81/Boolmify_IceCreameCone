    using Boolmify.Data;
    using Boolmify.Dtos.BannerDto;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

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
            return await _Context.Banners.OrderByDescending(b => b.CreatedAt).Select(b => new BannerDto
            {
                BannerId = b.BannerId,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                LinkUrl = b.LinkUrl,
                IsActive = b.IsActive,
                CreatedAt = b.CreatedAt,
            }).ToListAsync();
            
        }

        public async Task<BannerDto?> GetByIdBannerAsync(int id)
        {
            return await _Context.Banners.Where(b => b.BannerId == id).Select(b=>new BannerDto
            {
                BannerId = b.BannerId,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                LinkUrl = b.LinkUrl,
                IsActive = b.IsActive,
                CreatedAt = b.CreatedAt
            }).FirstOrDefaultAsync();
        }

        public async Task<BannerDto> CreateBannerAsync(CreateBannerDto dto)
        {
            var banner = new Banner
            {
                Title = dto.Title,
                ImageUrl = dto.ImageUrl,
                LinkUrl = dto.LinkUrl,
                IsActive = dto.IsActive
            };
            await _Context.Banners.AddAsync(banner);
            await _Context.SaveChangesAsync();
            return    await GetByIdBannerAsync(banner.BannerId) ?? throw new Exception("Failed to create banner");
        }
        

        public async Task<BannerDto?> UpdateBannerAsync(UpdateBannerDto dto)
        {
            var banner = await _Context.Banners.FindAsync(dto.BannerId);
            
            if (banner == null) return null;
            if (!string.IsNullOrWhiteSpace(dto.Title)) banner.Title = dto.Title;
            if (!string.IsNullOrWhiteSpace(dto.ImageUrl)) banner.ImageUrl = dto.ImageUrl;
            if (!string.IsNullOrWhiteSpace(dto.LinkUrl)) banner.LinkUrl = dto.LinkUrl;
            if (dto.IsActive.HasValue) banner.IsActive = dto.IsActive.Value;
            
            await _Context.SaveChangesAsync();
            return await GetByIdBannerAsync(banner.BannerId);
        }

        public async Task<bool> DeleteBannerAsync(int id)
        {
            var banner = await _Context.Banners.FindAsync(id);
            if (banner == null) return false;
            _Context.Banners.Remove(banner);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> toggleActiveBannerAsync(int id)
        {
            var banner = await _Context.Banners.FindAsync(id);
            if (banner == null) return false;
            banner.IsActive = !banner.IsActive;
            await _Context.SaveChangesAsync();
            return true;
        }
        
    }