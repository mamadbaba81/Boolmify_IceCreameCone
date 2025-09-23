    using Boolmify.Dtos.BannerDto;
    using Boolmify.Interfaces.ADminRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    namespace Boolmify.Controllers;
    [ApiController]
    [Route("Api/Admin/Banner")]
    [Authorize(Roles = "Admin")]
    public class AdminBannerController:ControllerBase
    {
        private readonly IAdminBannerService _bannerService;
    
        public AdminBannerController(IAdminBannerService bannerService)
        {
            _bannerService = bannerService;
        }
    
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<BannerDto>>> GetAllBannersAsync()
        {
            var banners = await _bannerService.GetAllBannersAsync();
            return Ok(banners);
        }
        
        
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BannerDto?>> GetByIdBannerAsync(int id)
        {
            var banner = await _bannerService.GetByIdBannerAsync(id);
            if (banner == null) return NotFound("Banner not found");
            return Ok(banner);
        }
    
        [HttpPost("Create")]
        public async Task<ActionResult<BannerDto>> CreateBannerAsync(CreateBannerDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var banner = await _bannerService.CreateBannerAsync(dto);
            return CreatedAtRoute("GetById",new {id = banner.BannerId},  banner);
        }
    
        [HttpPut("Update")]
        public async Task<ActionResult<BannerDto?>> UpdateBannerAsync ( int id ,UpdateBannerDto dto)
        {
            var updated =  await _bannerService.UpdateBannerAsync(id , dto);
            if (updated == null) return NotFound("Banner not found");
            return Ok(updated);
        }
    
        [HttpPatch("ToogleBanner/{id}")]
        public async Task<IActionResult> ToggleActiveBannerAsync(int id)
        {
            var result = await _bannerService.toggleActiveBannerAsync(id);
            if (!result) return NotFound("Banner not found");
            return Ok("Banner status updated");

        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBannerAsync(int id)
        {
            var deleted = await _bannerService.DeleteBannerAsync(id);
            if(!deleted) return NotFound("Banner not found");
            return Ok("Banner deleted successfully");
        }
        
        
        
    }