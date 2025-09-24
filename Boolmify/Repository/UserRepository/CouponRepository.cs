    using AutoMapper;
    using Boolmify.Data;
    using Boolmify.Dtos.Coupon;
    using Boolmify.Interfaces.USerService;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class CouponRepository:ICouponService
    {
        private readonly ApplicationDBContext  _Context;
        private readonly IMapper _mapper;

        public CouponRepository(ApplicationDBContext context ,  IMapper mapper)
        {
            _Context = context;
        }
        public async Task<CouponDto> ApplyCouponAsync(int id, string code)
        {
            var coupon = await _Context.Coupons.FirstOrDefaultAsync
                (c=>c.Code == code && c.IsActive && c.ValidFrom <= DateTime.Now && c.ValidTo >= DateTime.Now);
            if (coupon == null) return null;
           return _mapper.Map<CouponDto>(coupon);
            
        }
    }