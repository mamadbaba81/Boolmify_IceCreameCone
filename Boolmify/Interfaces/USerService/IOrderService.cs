    using Boolmify.Dtos.Order;
    using Boolmify.Models;

    namespace Boolmify.Interfaces.USerService;

    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(int UserId ,CreateOrderDto dto);

        Task<IEnumerable<OrderDto>> GetMyOrdersAsync(int id);
        
        Task<OrderDto?> GetOrderByIdAsync(int UserId ,int OrderId);
        
        
    }