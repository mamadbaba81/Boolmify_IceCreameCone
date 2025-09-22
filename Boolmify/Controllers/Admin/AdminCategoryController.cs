        using Boolmify.Dtos.Category;
        using Boolmify.Interfaces.ADminRepository;
        using Microsoft.AspNetCore.Authorization;
        using Microsoft.AspNetCore.Mvc;

        namespace Boolmify.Controllers;
            [ApiController]
            [Route("api/admin/category")]
            [Authorize(Roles = "Admin")]
        public class AdminCategoryController:ControllerBase
        {
            private readonly IAdminCategoryService _CategoryService;

            public AdminCategoryController(IAdminCategoryService categoryService)
            {
                _CategoryService = categoryService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(string? search = null, int pageNumber = 1, int pageSize = 10)
            {
                var category = await _CategoryService.GetAllAsync(search, pageNumber, pageSize);
                return Ok(category);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<CategoryDto>> GetByIdCategoryAsync(int id)
            {
                var category = await _CategoryService.GetByIdAsync(id);
                if (category == null) return NotFound("Category not found");
                return Ok(category);
            }

            [HttpGet("tree")]
            public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllTreeAsync()
            {
                var tree = await _CategoryService.GetTreeAsync();
                return Ok(tree);
            }

            [HttpPost]
            public async Task<ActionResult<CategoryDto>> AddCategoryAsync([FromBody] CreateCategoryDto dto)
            {
                var Category = await _CategoryService.CreateAsync(dto);
                if (Category == null) return BadRequest("Category not found");
                return Ok(Category);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<CategoryDto>> UpdateCategoryAsync(int id , [FromBody] UpdateCategoryDto dto)
            {
                var update =  await _CategoryService.UpdateAsync(id , dto);
                if (update == null) return BadRequest("Category not found");
                return Ok(update);
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<CategoryDto>> DeleteCategoryAsync(int id)
            {
                var delete = await _CategoryService.DeleteAsync(id);
                if (delete == null) return BadRequest("Category not found");
                return NoContent();
            }
            
            
        }