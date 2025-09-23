    using Boolmify.Data;
    using Boolmify.Dtos.Coupon;
    using Boolmify.Interfaces.ADminRepository;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.AdminRepository;

    public class AdminCouponRepository:IAdminCouponService
    {
        private readonly ApplicationDBContext  _Context;

        public AdminCouponRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<CouponDto>> GetAllAsync(string? search = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _Context.Coupons.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search)) query = query.Where(c=>c.Code.Contains(search));

            return await query.OrderByDescending(c => c.CouponId)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(c => new CouponDto
                {
                    CouponId = c.CouponId,
                    Code = c.Code,
                    DiscountType = c.DiscountType.ToString(),
                    Value = c.Value,
                    MaxDiscountAmount = c.MaxDiscountAmount,
                    MinOrderAmount = c.MinOrderAmount,
                    ValidForm = c.ValidFrom,
                    ValidTo = c.ValidTo,
                    IsActive = c.IsActive
                }).ToListAsync();

        }

        public async Task<CouponDto?> GetByIdAsync(int id)
        {
           var coupon = await _Context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
           if (coupon == null) return null;
           return new CouponDto
           {
               CouponId = coupon.CouponId,
               Code = coupon.Code,
               DiscountType = coupon.DiscountType.ToString(),
               Value = coupon.Value,
               MaxDiscountAmount = coupon.MaxDiscountAmount,
               MinOrderAmount = coupon.MinOrderAmount,
               ValidForm = coupon.ValidFrom,
               ValidTo = coupon.ValidTo,
               IsActive = coupon.IsActive
           };
        }

        public async Task<CouponDto> CreateAsync(CreateCouponDto dto)
        {
            if (!Enum.TryParse<DiscountType>(dto.DiscountType, true, out var discountType))
                throw new Exception("Invalid discount type");
            
            if (await _Context.Coupons.AnyAsync(c => c.Code == dto.Code))
                throw new Exception("Coupon code already exists.");

            var coupon = new Coupon
            {
                Code = dto.Code,
                DiscountType = discountType,
                Value = dto.Value,
                MaxDiscountAmount = dto.MaxDiscountAmount,
                MinOrderAmount = dto.MinOrderAmount,
                IsActive = true,
                ValidFrom = DateTime.Now
            };
            _Context.Coupons.Add(coupon);
            await _Context.SaveChangesAsync();
            return await GetByIdAsync(coupon.CouponId) ?? throw new Exception("Coupon creation failed");
        }

        public async Task<CouponDto> UpdateAsync(UpdateCouponDto dto)
        {
           var  coupon = await _Context.Coupons.FindAsync(dto.CouponId);
           if (coupon == null) return null;
           if (!string.IsNullOrWhiteSpace(dto.Code))  coupon.Code = dto.Code;
           if (!string.IsNullOrWhiteSpace(dto.DiscountType) &&
               Enum.TryParse<DiscountType>(dto.DiscountType, true, out var  discountType))
           {
               coupon.DiscountType = discountType;
           }
           if (dto.Value.HasValue) coupon.Value = dto.Value.Value;
           if (dto.MaxDiscountAmount.HasValue) coupon.MaxDiscountAmount = dto.MaxDiscountAmount.Value;
           if (dto.MinOrderAmount.HasValue) coupon.MinOrderAmount = dto.MinOrderAmount.Value;
           if (dto.ValidFrom.HasValue) coupon.ValidFrom = dto.ValidFrom.Value;
           if (dto.ValidTo.HasValue) coupon.ValidTo = dto.ValidTo.Value;
           if (dto.IsActive.HasValue) coupon.IsActive = dto.IsActive.Value;
           await _Context.SaveChangesAsync();
           return await GetByIdAsync(coupon.CouponId);
           
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var coupon = await _Context.Coupons.FindAsync(id);
            if (coupon == null) return false;
            _Context.Coupons.Remove(coupon);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(int id)
        {
            var  coupon = await _Context.Coupons.FindAsync(id);
            if (coupon == null) return false;
            
            coupon.IsActive = !coupon.IsActive;
            await _Context.SaveChangesAsync();
            return true ;
        }
    }