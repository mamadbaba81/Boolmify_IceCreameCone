    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Delivery;

    public class AssignDeliveryDto
    {
        [Required]
        public int  OrderId { get; set; }
        [Required]
        public int  CourierID { get; set; } 
    }