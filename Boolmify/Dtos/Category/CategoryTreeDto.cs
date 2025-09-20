    namespace Boolmify.Dtos.Category;

    public class CategoryTreeDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public string? Slug { get; set; }
        public List<CategoryTreeDto> Children { get; set; } = new();
    }