    using Boolmify.Dtos.Order;

    namespace Boolmify.Interfaces.ADminRepository;

    public interface IAdminOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync(string? status =  null , int? customerId = null);
        
        Task<OrderDto> GetByIdAsync(int id);
        
        Task<OrderDto> UpdateStatusAsync(int orderId , string newstatus);
        Task<bool> DeleteAsync(int id);
        
    }