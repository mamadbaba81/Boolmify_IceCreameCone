    namespace Boolmify.Dtos.Category;

    public class CategoryDto
    {
        public int  CategoryId { get; set; }

        public int?  ParentId { get; set; }

        public string  Name { get; set; } =  default!;

        public string?  Description { get; set; }

        public string?  Slug { get; set; }

        public List<CategoryDto> Children { get; set; } = new();
    }