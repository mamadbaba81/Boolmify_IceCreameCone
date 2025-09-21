    namespace Boolmify.Dtos.BannerDto;

    public class BannerDto
    {
        public int BannerId { get; set; }
        public string Title { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public string? LinkUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

    }