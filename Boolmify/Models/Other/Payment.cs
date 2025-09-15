        namespace Boolmify.Models;

        public class Payment
        {
            public int  PaymentId { get; set; }

            public int  OrderId { get; set; }

            public Order  Order { get; set; } = default!;

            public decimal Amount { get; set; }

            public PaymentMethod Method { get; set; } =  PaymentMethod.Online;

            public PaymentStatus  Status { get; set; } =  PaymentStatus.Pending;

            public DateTime  CreateAt { get; set; }=  DateTime.Now;

            public DateTime  PaidAt { get; set; } 
            
            public string? TransactionId { get; set; }
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
            Online,
            CashOnDelivery,
        }