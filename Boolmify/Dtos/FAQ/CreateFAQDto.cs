    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.FAQ;

    public class CreateFAQDto
    {
        [Required]
        [StringLength(200)]
        public string Question { get; set; } = default!;
        [Required]
        [StringLength(1000)]
        public string Answer { get; set; } = default!;
        
        public bool  IsActive { get; set; } 
    }