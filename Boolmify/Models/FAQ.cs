    namespace Boolmify.Models;

    public class FAQ
    {
        public int  FaqId { get; set; }

        public string Question { get; set; }
        
        public string Answer { get; set; }
        
        public DateTime  CreatedAt { get; set; } =  DateTime.Now;
    }