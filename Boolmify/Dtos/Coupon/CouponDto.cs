    namespace Boolmify.Dtos.Coupon;

    public class CouponDto
    {
        public int  CouponId { get; set; }
        public string  Code { get; set; } =  default!;
        public string  DiscountType { get; set; }= default!;
        public decimal  Value { get; set; }

        public decimal?  MaxDiscountAmount { get; set; }
        public decimal?  MinOrderAmount { get; set; }

        public DateTime?  ValidForm { get; set; }
        public DateTime?  ValidTo { get; set; }

        public bool  IsActive { get; set; }
        
        
    }