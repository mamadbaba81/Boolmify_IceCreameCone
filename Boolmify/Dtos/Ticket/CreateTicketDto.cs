    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Ticket;

    public class CreateTicketDto
    {
        [Required]
        public int  UserId { get; set; }
        
        [Required]
        public int OrderId { get; set; } // اجباری نیست، ممکنه تیکت عمومی باشه

        [Required]
        [StringLength(100)]
        public string Subject { get; set; } = default!;

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = default!;
        
        
    }