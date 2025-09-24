    using Boolmify.Dtos.Cart;
    using Boolmify.Models;

    namespace Boolmify.Interfaces.USerService;

    public interface ICartService
    {
        Task<CartDto> GetCartByUserIdAsync(int id);
        Task<CartDto> AddToCartAsync(int UserId , int ProductId , int Quantity);
        Task<CartDto> RemoveFromCartAsync(int UserId , int ProductId );
        Task<CartDto> UpdateQuantityAsync(int UserId , int ProductId , int Quantity);
    }