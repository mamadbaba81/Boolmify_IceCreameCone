    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Models;

    public class Banner
    {
        public int  BannerId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = default!;
        [Required]
        [MaxLength(500)]
        public string  ImageUrl { get; set; }  = default!;
        [MaxLength(500)]
        public string?  LinkUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } =  DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; } 
    }