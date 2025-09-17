    namespace Boolmify.Dtos.Ticket;

    public class TicketMessageDto
    {
        public int MessageId { get; set; }
        public int TicketId { get; set; }
        public int SenderId { get; set; }      // کاربری که پیام رو فرستاده (ادمین یا کاربر)
        public string SenderName { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }