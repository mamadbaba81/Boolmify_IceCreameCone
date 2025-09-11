    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Review;

    public class ReviewDto
    {
        public int ReviewId { get; set; }
       
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public string UserName { get; set; } = default!; // فقط نام کاربر

        public DateTime CreatedAt { get; set; }
    }