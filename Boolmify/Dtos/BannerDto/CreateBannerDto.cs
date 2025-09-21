    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.BannerDto;

    public class CreateBannerDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = default!;

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = default!;

        [MaxLength(500)]
        public string? LinkUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }