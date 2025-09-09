        namespace Boolmify.Models;

        public class Payment
        {
            public int  PaymentId { get; set; }

            public int  OrderId { get; set; }

            public Order  Order { get; set; } = default!;

            public decimal Amount { get; set; }

            public PaymentMethod Method { get; set; }

            public PaymentStatus  Status { get; set; }

            public DateTime  PaidAt { get; set; } =  DateTime.Now;
            
        }

        public enum PaymentStatus
        {
         Pending,
         Completed,
         Failed,
         Refunded,
            
        }

        public enum PaymentMethod
        {
            CreditCard,
            DebitCard,
            CashOnDelivery,
        }