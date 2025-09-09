    namespace Boolmify.Models;

    public class Coupon
    {
        public int  CouponId { get; set; }

        public string  Code { get; set; }

        public string  DiscountType { get; set; }

        public decimal  Value { get; set; }

        public DateTime ExpiryDate { get; set; } 
    }