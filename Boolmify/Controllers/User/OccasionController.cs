    using Boolmify.Dtos.Category;
    using Boolmify.Interfaces.USerService;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers.User;
    [ApiController]
    [Route("Api/User/Occasion")]
    public class OccasionController:ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public OccasionController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var resualt = await _categoryService.GetAllCategoriesAsync();
            return Ok(resualt);
            
        }
        
    }