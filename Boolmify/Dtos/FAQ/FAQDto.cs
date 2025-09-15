    namespace Boolmify.Dtos.FAQ;

    public class FAQDto
    {
        public int FaqId { get; set; }
        
        public string Question { get; set; } = default!;
        
        public string Answer { get; set; } = default!;
        
        public DateTime CreatedAt { get; set; }
    }