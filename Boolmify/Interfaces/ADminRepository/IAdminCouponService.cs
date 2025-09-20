    using Boolmify.Dtos.Coupon;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminCouponService
    {
        Task<IEnumerable<CouponDto>> GetAllAsync(string? search = null, int pageNumber = 1, int pageSize = 10);
        
        Task<CouponDto?> GetByIdAsync(int id);
        
        Task<CouponDto> CreateAsync(CreateCouponDto dto);
        
        Task<CouponDto> UpdateAsync(UpdateCouponDto dto);
        
        Task<bool> DeleteAsync(int id);
        
        Task<bool> ToggleActiveAsync(int id );
        
        
    }