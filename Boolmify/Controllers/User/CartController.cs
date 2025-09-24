    using System.Security.Claims;
    using Boolmify.Dtos.Cart;
    using Boolmify.Interfaces.USerService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Boolmify.Controllers.User;
    [ApiController]
    [Route("[controller]")]
    [Authorize("User,Admin")]
    public class CartController: ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }


        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var userId =  GetUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null) return NotFound(new { message = "Cart not found" });
            return Ok(cart);
        }
        
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            var userId = GetUserId();
             await _cartService.AddToCartAsync(userId , dto.ProductId , dto.Quantity);
             return Ok(new { message = "Product added to cart" });
        }

        [HttpPost("Remove/{id}")]
        public async Task<IActionResult> RemoveFromCart([FromRoute] int id)
        {
            var userId = GetUserId();
           await _cartService.RemoveFromCartAsync(userId , id);
            return Ok(new { message = "Product removed from cart" });
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] AddToCartDto dto)
        {
            var userId = GetUserId();
            await _cartService.UpdateQuantityAsync(userId , dto.ProductId , dto.Quantity);
            return Ok(new { message = "Product updated" });
        }
    }