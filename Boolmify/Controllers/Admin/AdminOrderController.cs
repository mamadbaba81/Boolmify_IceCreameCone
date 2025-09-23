    using Boolmify.Dtos.Order;
    using Boolmify.Interfaces.ADminRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers;
        [ApiController]
        [Route("Api/Admin/Order")]
        [Authorize(Roles = "Admin")]
    public class AdminOrderController: ControllerBase
    {
        private readonly IAdminOrderService _orderService;

        public AdminOrderController(IAdminOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders(int pageNumber = 1, int pageSize = 10, int? userId = null, string? status = null)
        {
            var  orders = await _orderService.GetAllAsync(pageNumber, pageSize, userId, status);
            return Ok(orders);
        }

        [HttpGet("GetById{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound("Order not found");
            return Ok(order);
        }

        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> UpdateStatusOrder(int id ,[FromQuery] string newStatus )
        {
            var updated = await _orderService.UpdateStatusAsync(id, newStatus);
            if (updated == null) return NotFound("Order not found");
            return Ok("Order status updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var  deleted = await _orderService.DeleteAsync(id);
            if (!deleted) return NotFound("Order not found");
            return Ok("Order Deleted Successfully");
        }
    }