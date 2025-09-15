    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Ticket;

    public class UpdateTicketStatusDto
    {
        [Required]
        public int  TicketId { get; set; }
        [Required]
        public string  Status { get; set; } =  default!;
        
    }