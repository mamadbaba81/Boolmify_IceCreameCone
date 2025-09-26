    using Boolmify.Dtos.Category;
    using Boolmify.Models;

    namespace Boolmify.Interfaces.USerService;

    public interface ICategoryService
    {
        Task<List<CategoryTreeDto>> GetAllCategoriesAsync();
    }