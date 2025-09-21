    using Boolmify.Dtos.Coupon;
    using Boolmify.Interfaces.ADminRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers;
    [ApiController]
    [Route("api/Admin/Coupon")]
    [Authorize(Roles = "Admin")]
    public class AdminCouponController:ControllerBase
    {
        private readonly IAdminCouponService _couponService;

        public AdminCouponController(IAdminCouponService couponService)
        {
            _couponService = couponService;   
        }

        [HttpGet("GetAllCoupons")]
        public async Task<ActionResult<IEnumerable<CouponDto>>> GetAllCouponsAsync([FromQuery] string? search = null,
            int pageNumber = 1, int pageSize = 10)
        {
            var  coupons = await _couponService.GetAllAsync(search, pageNumber, pageSize);
            return Ok(coupons);
        }

        [HttpGet("GetByIdCoupon/{id}")]
        public async Task<ActionResult<CouponDto>> GetById(int id)
        {
            var coupon = await _couponService.GetByIdAsync(id);
            if (coupon == null) return NotFound("Coupon not found");
            return Ok(coupon);
        }

        [HttpPost("CreateCoupon")]
        public async Task<ActionResult<CouponDto>> CreateCouponAsync([FromBody] CreateCouponDto dto)
        {
            var coupon = await _couponService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = coupon.CouponId},  coupon);
            
        }

        [HttpPut("UpdateCoupon")]
        public async Task<ActionResult> UpdateCouponAsync(int id , [FromBody] UpdateCouponDto dto)
        {
            if (id != dto.CouponId)  return BadRequest("ID mismatch");
            var updated = await _couponService.UpdateAsync(dto);
            if (updated == null) return NotFound("Coupon not found");
            await _couponService.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("DeleteCoupon/{id}")]
        public async Task<ActionResult> DeleteCouponAsync(int id)
        {
            var deleted = await _couponService.DeleteAsync(id);
            if(!deleted) return NotFound("Coupon not found");
             return NoContent();
        }

        [HttpPatch("ToggleCoupon/{id}")]
        public async Task<ActionResult> ToggleActive(int id)
        {
            var result = await _couponService.ToggleActiveAsync(id);
            if (!result) return NotFound("Coupon not found");
            return Ok("Coupon status toggled");
        }
    }