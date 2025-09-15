    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.AddOn;

    public class CreateProductAddonDto
    {
        [Required]
        [StringLength(100)]
        public string ProductAddOnName { get; set; }
        [Required]
        [Range(0.0 , double.MaxValue , ErrorMessage = "Price must be a positive value.")]
        public decimal  Price { get; set; }
        
    }