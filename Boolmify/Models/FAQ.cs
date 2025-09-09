    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Models;

    public class FAQ
    {
        public int  FaqId { get; set; }
        
        [Required]
        public string Question { get; set; } = default!;
        
        [Required]
        public string Answer { get; set; } = default!;
        
        public DateTime  CreatedAt { get; set; } =  DateTime.Now;
    }