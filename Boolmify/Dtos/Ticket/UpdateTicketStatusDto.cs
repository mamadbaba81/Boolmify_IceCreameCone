    using System.ComponentModel.DataAnnotations;
    using Boolmify.Models;

    namespace Boolmify.Dtos.Ticket;

    public class UpdateTicketStatusDto
    {
        [Required]
        public int  TicketId { get; set; }
        [Required]
        public TicketStatus  Status { get; set; } 
        
    }