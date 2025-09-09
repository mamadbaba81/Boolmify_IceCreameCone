    namespace Boolmify.Models;

    public class Ticket
    {
        public int  TicketId { get; set; }

        public int  UserId { get; set; }

        public AppUser  AppUser { get; set; } = default!;

        public int  OrderId { get; set; }

        public Order  Order { get; set; }

        public string  Subject { get; set; }

        public string  Message { get; set; }

        public TicketStatus  Status  { get; set; } =  TicketStatus.open;

        public DateTime  CreatedAt { get; set; } =  DateTime.Now;
        
        
        
    }

    public enum TicketStatus
    {
        open,
        Inprogress,
        closed
    }