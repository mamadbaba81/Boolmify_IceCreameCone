    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Models;

    public class Coupon
    {
        public int  CouponId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string  Code { get; set; } =  default!;
        // vaghti meghdar nmidi default  mishe avalin enum
        
        public DiscountType  DiscountType { get; set; } 
        [Range(0 , double.MaxValue)]
        public decimal  Value { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public List<Order> Orders { get; set; } = new();
        
        public List<CouponRedemption> CouponRedemption { get; set; } = new();

    }

    public enum DiscountType
    {
        Percentage,
        Fixed
    }