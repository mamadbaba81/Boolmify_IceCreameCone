    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.AddOn;

    public class ProductAddOnUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }
    }