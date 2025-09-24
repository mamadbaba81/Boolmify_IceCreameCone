    using AutoMapper;
    using Boolmify.Data;
    using Boolmify.Dtos.Order;
    using Boolmify.Interfaces.USerService;
    using Boolmify.Models;

    namespace Boolmify.Repository.UserRepository;

    public class OrderRepository:IOrderService
    {
        private readonly ApplicationDBContext  _Context;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;

        public OrderRepository(ApplicationDBContext context,  IMapper mapper, ICartService cartService)
        {
            _Context = context;
            _mapper = mapper;
            _cartService = cartService;
        }
        public async Task<OrderDto> CreateOrderAsync(int userId, CreateOrderDto dto)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null || !cart.Items.Any()) throw new Exception("Cart is empty"); 
            var totalAmount = cart.Items.Sum(i => i.Quantity * i.Product.Price);
            decimal discountAmount = 0;
            decimal finalAmount = totalAmount - discountAmount;
            var order = new Order
            {
                UserId = userId,
                RecipientName = dto.RecipientName,
                RecipientPhone = dto.RecipientPhone,
                RecipientAddress = dto.RecipientAddress,
                DeliveryDate = dto.DeliveryDate,
                Status = OrderStatus.Pending,
                TotalAmount = totalAmount,
                DiscountAmount = discountAmount,
                FinalAmouont = finalAmount,
                CreateAt = DateTime.UtcNow,
                OrderItems = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.Product.Price,
                }).ToList()
            };
            await _Context.Orders.AddAsync(order);
            await _Context.SaveChangesAsync();
            cart.Items.Clear();
            await _Context.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetMyOrdersAsync(int id)
        {
            var orders = await _cartService.GetCartByUserIdAsync(id);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int UserId, int OrderId)
        {
            var  order = await _cartService.GetCartByUserIdAsync(UserId);
            if (order == null) return null;
            return _mapper.Map<OrderDto>(order);
        }
    }