    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Ticket;

    public class CreateTicketMessageDto
    {
        [Required]
        public int  SenderId { get; set; }
        
        [Required]
        [StringLength(2000)]
        public string Content { get; set; } = default!;
    }