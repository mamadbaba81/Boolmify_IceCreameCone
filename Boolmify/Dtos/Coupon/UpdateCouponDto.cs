    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Coupon;

    public class UpdateCouponDto
    {
        [Required]
        public int CouponId { get; set; }

        [StringLength(20)]
        public string? Code { get; set; }

        public string? DiscountType { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal? Value { get; set; }

        public decimal? MaxDiscountAmount { get; set; }
        public decimal? MinOrderAmount { get; set; }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public bool? IsActive { get; set; }
    }