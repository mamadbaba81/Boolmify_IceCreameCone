    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.BannerDto;

    public class UpdateBannerDto
    {
        [Required]
        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        [MaxLength(500)]
        public string? LinkUrl { get; set; }

        public bool? IsActive { get; set; }
    }