    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Category;

    public class UpdateCategoryDto
    {
        [Required]
        public int  CategoryId { get; set; }
        [StringLength(100)]
        public string?  Name { get; set; } =  default!;
        [StringLength(500)]
        public string?  Description { get; set; }

        public string?  Slug { get; set; }

        public int?   ParentId { get; set; }
        
    }