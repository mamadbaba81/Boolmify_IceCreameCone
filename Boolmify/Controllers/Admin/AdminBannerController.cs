    using Boolmify.Dtos.BannerDto;
    using Boolmify.Interfaces.ADminRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    namespace Boolmify.Controllers;
    [ApiController]
    [Route("api/banner")]
    [Authorize(Roles = "Admin")]
    public class AdminBannerController:ControllerBase
    {
        private readonly IAdminBannerService _bannerService;
    
        public AdminBannerController(IAdminBannerService bannerService)
        {
            _bannerService = bannerService;
        }
    
        [HttpGet("GetAllBanners")]
        public async Task<List<BannerDto>> GetAllBannersAsync()
        {
            
        }
        
        
        [HttpGet("GetByIdBanner/{id}")]
        public async Task<BannerDto?> GetBannerAsync(int id)
        {
            
        }
    
        [HttpPost("CreateBanner")]
        public async Task<BannerDto> CreateBannerAsync(CreateBannerDto BannerDto)
        {
            
        }
    
        [HttpPut("UpdateBanner")]
        public async Task<BannerDto?> UpdateBannerAsync(UpdateBannerDto BannerDto)
        {
            
        }
    
        [HttpPatch("ToogleBanner/{id}")]
        public async Task<BannerDto?> ToggleActiveBannerAsync(int id)
        {
            
            
        }
        
        [HttpDelete("DeleteBanner/{id}")]
        public async Task<bool> DeleteBannerAsync(int id)
        {
            
        }
        
        
        
    }