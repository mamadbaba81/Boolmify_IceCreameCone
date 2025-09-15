    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Review;

    public class UpdateReviewDto
    {
        [Required]
        public int  ReviewId { get; set; }
        [Range(1,5)]
        public int  Rating { get; set; }
        [StringLength(1000)]
        public string?  Comment { get; set; }
        
        
    }