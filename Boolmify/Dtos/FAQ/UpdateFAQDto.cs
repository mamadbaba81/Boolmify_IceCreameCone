    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.FAQ;

    public class UpdateFAQDto
    {
        [Required]
        public int FaqId { get; set; }

        [StringLength(200)]
        public string? Question { get; set; }

        [StringLength(2000)]
        public string? Answer { get; set; }
    }