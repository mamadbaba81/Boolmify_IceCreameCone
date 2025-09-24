    using Boolmify.Models;

    namespace Boolmify.Interfaces.USerService;

    public interface ICartService
    {
        Task<Cart?> GetCartByUserIdAsync(int id);
        Task AddToCartAsync(int UserId , int ProductId , int Quantity);
        Task RemoveFromCartAsync(int UserId , int ProductId );
        Task UpdateQuantityAsync(int UserId , int ProductId , int Quantity);
        Task SaveChangesAsync();
    }