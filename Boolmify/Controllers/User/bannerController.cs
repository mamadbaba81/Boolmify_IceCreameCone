    using Boolmify.Interfaces.USerService;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers.User;
    [ApiController]
    [Route("Api/User/Banner")]
    public class bannerController: ControllerBase
    {
        private readonly IBannerService _bannerService;

        public bannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBannersAsync()
        {
            var banner = await _bannerService.GetAllBannersAsync();
            return Ok(banner);
            
        }
    }
    