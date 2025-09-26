    using Boolmify.Data;
    using Boolmify.Dtos.Category;
    using Boolmify.Interfaces.USerService;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class CategoryRepository: ICategoryService
    {
        
        private readonly ApplicationDBContext  _Context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        
        private CategoryTreeDto MapToTreeDto(Models.Category category)
        {
            return new CategoryTreeDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Slug = category.Slug,
                Children = category.Children.Select(MapToTreeDto).ToList()
            };
        }
        public async Task<List<CategoryTreeDto>> GetAllCategoriesAsync()
        {
            var category = await _Context.Categories.Include
                (c=>c.Children).Where(c=>c.ParentId==null).ToListAsync();

            return category.Select(MapToTreeDto).ToList();
        }
        
    }