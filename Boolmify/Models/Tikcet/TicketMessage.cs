    namespace Boolmify.Models;

    public class TicketMessage
    {
        public int TicketMessageId { get; set; }

        // متن پیام
        public string Message { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ارتباط با Ticket
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;

        // ارتباط با User (فرستنده)
        public int SenderId { get; set; }
        public AppUser Sender { get; set; } = default!;
    }