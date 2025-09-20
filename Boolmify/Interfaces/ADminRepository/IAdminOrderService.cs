    using Boolmify.Dtos.Order;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, int? userId = null, string? status = null);
        
        Task<OrderDto> GetByIdAsync(int id);
        
        Task<OrderDto> UpdateStatusAsync(int id , string newStatus);
        Task<bool> DeleteAsync(int id);
        
    }