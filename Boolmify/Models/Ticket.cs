    namespace Boolmify.Models;

    public class Ticket
    {
        public int  TicketId { get; set; }

        public int  UserId { get; set; }

        public int  OrderId { get; set; }

        public string  Subject { get; set; }

        public string  Massage { get; set; }

        public string  Status  { get; set; }

        public DateTime  CreatedAt { get; set; } =  DateTime.Now;
        
    }