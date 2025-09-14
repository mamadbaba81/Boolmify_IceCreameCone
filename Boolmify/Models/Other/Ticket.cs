    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Models;

    public class Ticket
    {
        public int  TicketId { get; set; }

        public int  UserId { get; set; }

        public AppUser  User { get; set; } = default!;

        public int  OrderId { get; set; }

        public Order  Order { get; set; }
        
        [Required , MaxLength(200)]
        public string  Subject { get; set; } = default!;
        
        [Required]
        public string  Message { get; set; } = default!;

        public TicketStatus  Status  { get; set; } =  TicketStatus.Open;

        public DateTime  CreatedAt { get; set; } =  DateTime.Now;

        public DateTime?  UpdateAt { get; set; }
        
        
        
    }

    public enum TicketStatus
    {
        Open,
        Inprogress,
        Closed
    }