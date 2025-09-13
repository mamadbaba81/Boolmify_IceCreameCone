    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Coupon;

    public class ApplyCouponDto
    {
        [Required]
        public int OrderId { get; set; } // یا CartId اگر بخوای کوپن قبل از سفارش هم اعمال بشه

        [Required]
        [StringLength(20)]
        public string CouponCode { get; set; } = default!;
    }