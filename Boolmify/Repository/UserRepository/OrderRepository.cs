    using AutoMapper;
    using Boolmify.Data;
    using Boolmify.Dtos.Order;
    using Boolmify.Interfaces.USerService;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class OrderRepository:IOrderService
    {
        private readonly ApplicationDBContext  _Context;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDBContext context,  IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
        public async Task<OrderDto> CreateOrderAsync(int userId, CreateOrderDto dto)
        {
          var cart = await _Context.Carts.Include(c=>c.Items)
              .ThenInclude(i => i.Product).FirstOrDefaultAsync(c=>c.USerId == userId);
          if (cart == null || !cart.Items.Any()) throw new Exception("No cart found");
          var totalAmount = cart.Items.Sum(i => i.Quantity * i.Product.Price);
          var order = new Order
          {
              UserId = userId,
              RecipientName = dto.RecipientName,
              RecipientPhone = dto.RecipientPhone,
              RecipientAddress = dto.RecipientAddress,
              DeliveryDate = dto.DeliveryDate,
              Status = OrderStatus.Pending,
              TotalAmount = totalAmount,
              DiscountAmount = 0, // بعداً اگر کوپن اعمال شد تغییر می‌کنه
              FinalAmouont = totalAmount,
              CreateAt = DateTime.UtcNow,
              OrderItems = cart.Items.Select(i => new OrderItem
              {
                  ProductId = i.ProductId,
                  Quantity = i.Quantity,
                  UnitPrice = i.Product.Price
              }).ToList()
          };  
          await _Context.Orders.AddAsync(order);
          cart.Items.Clear();
          await _Context.SaveChangesAsync();
          
          return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetMyOrdersAsync(int id)
        {
            var orders = await _Context.Orders.Include(o=>o.OrderItems)
                .ThenInclude(oi=>oi.Product).Where(o=>o.UserId == id).OrderByDescending(o=>o.CreateAt)
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int UserId, int OrderId)
        {
            var order = await _Context.Orders.Include(o=>o.OrderItems).ThenInclude(oi=>oi.Product)
                .FirstOrDefaultAsync(o=>o.UserId == UserId && o.OrderId == OrderId);
            return _mapper.Map<OrderDto?>(order);
        }
    }