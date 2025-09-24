    using System.Security.Claims;
    using Boolmify.Dtos.Order;
    using Boolmify.Interfaces.USerService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers.User;
    [ApiController]
    [Route("Api/Order")]
    [Authorize(Roles = "User , Admin")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("Checkout")]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var userId = GetUserId();
            var order = await _orderService.CreateOrderAsync(userId, dto);
            if (order == null) return null;
            return Ok(order);
        }
//History
        [HttpGet("My/Orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderById(int id)
        {
            var userId = GetUserId();
            var order = await _orderService.GetMyOrdersAsync(userId);
            if (order == null) return null;
            return Ok(order);
        }

        [HttpGet("Orders/{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderByIdAsync(int id)
        {
            var userId = GetUserId();
            var order = await _orderService.GetOrderByIdAsync(id, userId);
            if (order == null) return NotFound(new { message = "Order not found" });
            return Ok(order);
        }
        
    }