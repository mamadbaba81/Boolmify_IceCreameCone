    using System.ComponentModel.DataAnnotations.Schema;

    namespace Boolmify.Models;

    public class Category
    {
        public int  CategoryId { get; set; }
        // برای دسته‌بندی تو در تو (nullable)
        public int? ParentId { get; set; }
        
        [ForeignKey(nameof(ParentId))]
        public Category? Parent { get; set; }
        
        public virtual List<Category> Children { get; set; } = new();
        
        public string  Name { get; set; } = default!;
        
        public string?  Description { get; set; }

        public string?  Slug { get; set; }

        public virtual List<Product> Products { get; set; } = new();
    }