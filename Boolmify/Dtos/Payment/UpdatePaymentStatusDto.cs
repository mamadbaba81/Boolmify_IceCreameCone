    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Payment;

    public class UpdatePaymentStatusDto
    {
        [Required]
        public int  paymentId { get; set; }
        [Required]
        public string  Status { get; set; } =  default!;
    }