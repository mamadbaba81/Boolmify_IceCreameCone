    namespace Boolmify.Models;

    public class Order
    {
        public int  OrderId { get; set; }

        public int UserId  { get; set; }
        
        public virtual AppUser User { get; set; }

        public string RecipientName { get; set; }
        
        public string RecipientPhone { get; set; }
        
        public string RecipientAddress { get; set; }

        public DateTime  DeliveryDate { get; set; }

        public OrderStatus Status { get; set; } =  OrderStatus.Pending;

        public decimal TotalAmount { get; set; }

        public DateTime CreateAt { get; set; } =  DateTime.Now;
        
        public virtual List<CartItem> CartItems { get; set; } = new();
        
        public List<CouponRedemption> CouponRedemptions { get; set; } = new();

        public Delivery  Delivery { get; set; } =  default!;
        
        public List<Payment> Payments { get; set; } = new();

        
    }

    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Delivered,
        Cancelled
    }

    