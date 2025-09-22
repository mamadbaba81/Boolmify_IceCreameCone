    using Boolmify.Data;
    using Boolmify.Dtos.Category;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.AdminRepository;

    public class AdminCategoryRepository : IAdminCategoryService
    {
        private readonly ApplicationDBContext _context;

        public AdminCategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(string? search = null, int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.Categories.Include(c => c.Children).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) query = query.Where(c => c.Name.Contains(search));

            return await query.OrderByDescending(c => c.CategoryId).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    ParentId = c.ParentId,
                    Name = c.Name,
                    Description = c.Description,
                    Slug = c.Slug,
                    Children = c.Children.Select(ch => new CategoryDto
                    {
                        CategoryId = ch.CategoryId,
                        ParentId = ch.ParentId,
                        Name = ch.Name,
                        Description = ch.Description,
                        Slug = ch.Slug
                    }).ToList()
                })
                .ToListAsync();

        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null) return null;

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                ParentId = category.ParentId,
                Name = category.Name,
                Description = category.Description,
                Slug = category.Slug,
                Children = category.Children.Select(ch => new CategoryDto
                {
                    CategoryId = ch.CategoryId,
                    ParentId = ch.ParentId,
                    Name = ch.Name,
                    Description = ch.Description,
                    Slug = ch.Slug
                }).ToList()

            };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                Slug = dto.Slug,
                ParentId = dto.ParentID
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(category.CategoryId) ?? throw new Exception("Category creation failed.");

        }

        public async Task<CategoryDto?> UpdateAsync( int id ,UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;
            if (!string.IsNullOrWhiteSpace(dto.Name)) category.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.Description)) category.Description = dto.Description;
            if (!string.IsNullOrWhiteSpace(dto.Slug)) category.Slug = dto.Slug;
            category.ParentId = dto.ParentId == 0 ? null : dto.ParentId;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(category.CategoryId) ?? throw new Exception("Category update failed.");

        }

        public async Task<IEnumerable<CategoryDto>> GetTreeAsync()
        {
            var category = await _context.Categories.Include(c => c.Children)
                .Where(c => c.ParentId == null).ToListAsync();

            return category.Select(c => MapToDtoRecursive(c)).ToList();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null) return false;
            if (category.Children.Any()) throw new Exception("Cannot delete category with child categories.");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        private CategoryDto MapToDtoRecursive(Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                ParentId = category.ParentId,
                Name = category.Name,
                Description = category.Description,
                Slug = category.Slug,
                Children = category.Children.Select(MapToDtoRecursive).ToList()
            };
        }

    }