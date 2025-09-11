    namespace Boolmify.Models;

    public class Order
    {
        public int  OrderId { get; set; }

        public int UserId  { get; set; }
        
        public virtual AppUser User { get; set; } = default!;

        public string RecipientName { get; set; } =  default!;
        
        public string RecipientPhone { get; set; } = default!;
        
        public string RecipientAddress { get; set; } =  default!;

        public DateTime  DeliveryDate { get; set; }

        public OrderStatus Status { get; set; } =  OrderStatus.Pending;

        public decimal TotalAmount { get; set; }

        public decimal  FinalAmouont { get; set; }

        public decimal  DiscountAmount { get; set; }

        public DateTime CreateAt { get; set; } =  DateTime.Now;
        
        public virtual List<OrderItem> OrderItems { get; set; } = new();
        
        public virtual CouponRedemption? CouponRedemptions { get; set; } 

        public virtual Delivery  Delivery { get; set; } 
        
        public virtual List<Payment> Payments { get; set; } = new();

        
    }

    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Delivered,
        Cancelled
    }

    