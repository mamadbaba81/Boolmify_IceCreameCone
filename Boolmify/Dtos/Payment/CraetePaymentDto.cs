    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Payment;

    public class CraetePaymentDto
    {
        [Required]
        public int  OrderId { get; set; }
        [Required]
        [Range(0.0 , double.MaxValue)]
        public decimal  Amount { get; set; }
        [Required]
        public string Method { get; set; } = default!;
       
    }