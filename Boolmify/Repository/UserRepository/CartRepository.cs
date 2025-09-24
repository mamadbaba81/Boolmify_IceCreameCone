    using Boolmify.Data;
    using Boolmify.Interfaces.USerService;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class CartRepository:ICartService
    {
        private readonly ApplicationDBContext  _Context;

        public CartRepository(ApplicationDBContext context)
        {
            _Context = context;
        }
        public async Task<Cart?> GetCartByUserIdAsync(int id)
        {
           return await _Context.Carts.Include(c=>c.Items).ThenInclude(i=>i.Product)
               .FirstOrDefaultAsync(c=>c.USerId == id);
        }

        public async Task AddToCartAsync(int UserId, int ProductId, int Quantity)
        {
            var cart = await GetCartByUserIdAsync(UserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    USerId = UserId,
                    Items = new List<CartItem>()
                };
                await _Context.Carts.AddAsync(cart);
            }
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += Quantity;
            }
            else
            {
                cart.Items.Add(new  CartItem
                {
                    ProductId = ProductId,
                    Quantity = Quantity
                });
            }
        }

        public async Task RemoveFromCartAsync(int UserId, int ProductId)
        {
            var  cart = await GetCartByUserIdAsync(UserId);
            if (cart == null) return;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == ProductId);
            if (item != null)
            {
                cart.Items.Remove(item);
            }
            
        }

        public async Task UpdateQuantityAsync(int UserId, int ProductId, int Quantity)
        {
            var  cart = await GetCartByUserIdAsync(UserId);
            if (cart == null) return;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == ProductId);
            if (item != null)
            {
                item.Quantity = Quantity;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }