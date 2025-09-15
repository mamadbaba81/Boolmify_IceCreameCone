    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Review;

    public class CreateReviewDto
    {
        [Required]
        public int  ProductId { get; set; }
        [Required]
        [Range(1, 5)]
        public int  Rating { get; set; }
        [StringLength(1000)]
        public string? Comment { get; set; }
    }