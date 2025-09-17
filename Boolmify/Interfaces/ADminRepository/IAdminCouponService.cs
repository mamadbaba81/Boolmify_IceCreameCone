    using Boolmify.Dtos.Coupon;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminCouponService
    {
        Task<IEnumerable<CouponDto>> GetAllAsync();
        
        Task<CouponDto?> GetByIdAsync(int id);
        
        Task<CouponDto> CreateAsync(CreateCouponDto dto);
        
        Task<CouponDto> UpdateAsync(UpdateCouponDto dto);
        
        Task<bool> DeleteAsync(int id);
        
        Task<CouponDto> ToggleActiveAsync(int id ,  bool isactive);
        
        
    }