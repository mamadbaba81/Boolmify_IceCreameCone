    using System.ComponentModel.DataAnnotations;

    namespace Boolmify.Dtos.Coupon;

    public class CreateCouponDto
    {
        [Required]
        [StringLength(20)]
        public string  Code { get; set; } =  default!;
        [Required]// باید تبدیل بشه به enum در سمت سرور
        public string  DiscountType { get; set; }= default!;
        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal  Value { get; set; }

        public decimal?  MaxDiscountAmount { get; set; }
        public decimal?  MinOrderAmount { get; set; }
        
    }