    namespace Boolmify.Models;

    public class CouponRedemption
    {
        public int  CouponId { get; set; }

        public Coupon  Coupon { get; set; } =  default!;

        public int  UserId { get; set; }

        public AppUser  AppUser { get; set; } = default!;

        public int  OrderId { get; set; }
        
        public Order Order { get; set; } = default!;
        
    }