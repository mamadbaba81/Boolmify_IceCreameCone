    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Models;

    public class Review
    {
        public int  ReviewId { get; set; }

        public int  ProductId { get; set; }
        
        public virtual Product Product { get; set; } = default!;

        public int  UserId { get; set; }
        
        public virtual AppUser User { get; set; } = default!;
        
        [Range(1,5)]
        public int  Rating { get; set; }

        public string?  Comment { get; set; }

        public DateTime  CreatedAt { get; set; } =  DateTime.Now;

        public DateTime?  UpdateAt { get; set; }
        
    }