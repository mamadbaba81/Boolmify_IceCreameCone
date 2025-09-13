    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Occasion;

    public class CreateOccasionDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;
        [StringLength(500)]
        public string?   description { get; set; }

        public string?  IconUrl { get; set; }
    }