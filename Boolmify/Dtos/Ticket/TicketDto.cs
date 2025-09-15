    namespace Boolmify.Dtos.Ticket;

    public class TicketDto
    {
        public int  TicketId { get; set; }
        public int  UserId { get; set; }
        public string UserName { get; set; } = default!;
        
        public int  OrderID { get; set; }
        
        public string  Subject { get; set; } = default!;
        public string  Message { get; set; } = default!;
        
        public string  Status { get; set; } =  default!;
        public DateTime  CreatedAt { get; set; }
        
        
    }