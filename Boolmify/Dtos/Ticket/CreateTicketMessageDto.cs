    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Ticket;

    public class CreateTicketMessageDto
    {
        [Required]
        public int TicketId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Message { get; set; } = default!;
    }