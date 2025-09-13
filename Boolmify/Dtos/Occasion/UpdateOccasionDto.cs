    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Occasion;

    public class UpdateOccasionDto
    {
        [Required]
        public int  OccasionId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string? Name { get; set; } = default!;
        [StringLength(500)]
        public string?   description { get; set; }

        public string?  IconUrl { get; set; }
    }