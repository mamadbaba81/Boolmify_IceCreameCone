    using Boolmify.Models;

    namespace Boolmify.Dtos.Occasion;

    public class OccasionDto
    {
        public bool IsActive { get; set; }
        public int  OccasionId { get; set; }
        public string Name { get; set; } = default!;
        public string?   description { get; set; }
        public string?  IconUrl { get; set; }

    }