    using Boolmify.Dtos.Coupon;

    namespace Boolmify.Interfaces.USerService;

    public interface ICouponService
    {
        Task<CouponDto> ApplyCouponAsync (int id , string code);
    }