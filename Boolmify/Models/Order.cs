    namespace Boolmify.Models;

    public class Order
    {
        public int  OrderId { get; set; }

        public int UserId  { get; set; }

        public string RecipientName { get; set; }
        
        public string RecipientPhone { get; set; }
        
        public string RecipientAddress { get; set; }

        public DateTime  DeliveryDate { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreateAt { get; set; } =  DateTime.Now;
        
        
        
        
    }