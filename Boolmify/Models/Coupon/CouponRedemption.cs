    namespace Boolmify.Models;

    public class CouponRedemption
    {
        public int  CouponId { get; set; }
        public virtual Coupon  Coupon { get; set; } =  default!;
        public int  UserId { get; set; }
        public virtual AppUser  user { get; set; } = default!;
        public int  OrderId { get; set; }
        public virtual Order Order { get; set; } = default!;
        
        public DateTime UsedAt { get; set; } = DateTime.Now;
        public decimal  DiscountAmount { get; set; }

    }