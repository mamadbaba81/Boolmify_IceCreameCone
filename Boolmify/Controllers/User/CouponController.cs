    using System.Security.Claims;
    using Boolmify.Dtos.Coupon;
    using Boolmify.Interfaces.USerService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers.User;
    [ApiController]
    [Route("Api/Coupon")]
    [Authorize(Roles = "Admin , Customer")]
    public class CouponController: ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("Apply")]
        public async Task<ActionResult<CouponDto>> ApplyCoupon(int id,[FromQuery] string code)
        {
            var userId = GetUserId();
            var coupon = await _couponService.ApplyCouponAsync(id, code);
            if (coupon == null) return BadRequest(new { message = "Invalid or expired coupon" });
            return Ok(coupon);
        }
        
    }