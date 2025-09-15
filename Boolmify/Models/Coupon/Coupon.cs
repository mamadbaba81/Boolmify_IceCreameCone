    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Models;
    using System.ComponentModel.DataAnnotations;
    public enum DiscountType
    {
        Percentage,
        Amount
    }
    public class Coupon
    {
        public int CouponId { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; } = default!;

        [Required]
        public DiscountType DiscountType { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Value { get; set; }  // مثلا 10% یا 50000 تومان

        // سقف تخفیف برای کوپن‌های درصدی
        public decimal? MaxDiscountAmount { get; set; }

        // حداقل مبلغ سفارش برای استفاده
        public decimal? MinOrderAmount { get; set; }

        // سقف دفعات استفاده برای کل کاربران
        public int? TotalUsageLimit { get; set; }

        // سقف دفعات استفاده برای هر کاربر
        public int? UsageLimitPerUser { get; set; }

        // اعتبار زمانی
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public bool IsActive { get; set; } = true;

        // روابط
        public virtual List<CouponRedemption> CouponRedemptions { get; set; } = new();
    }

    
  