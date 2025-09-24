    using AutoMapper;
    using Boolmify.Data;
    using Boolmify.Dtos.Cart;
    using Boolmify.Interfaces.USerService;
    using Boolmify.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Boolmify.Repository.UserRepository;

    public class CartRepository:ICartService
    {
        private readonly ApplicationDBContext  _Context;
        private readonly IMapper _mapper;

        public CartRepository(ApplicationDBContext context , IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
        public async Task<CartDto> GetCartByUserIdAsync(int id)
        {
           var cart =await _Context.Carts.Include(c=>c.Items).ThenInclude(i=>i.Product)
               .FirstOrDefaultAsync(c=>c.USerId == id);
           if (cart == null)
           {
               cart = new Cart { USerId = id };
               await _Context.Carts.AddAsync(cart);
               await _Context.SaveChangesAsync();
           }
           return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> AddToCartAsync(int UserId, int ProductId, int Quantity)
        {
            var cart = await _Context.Carts.Include(c=>c.Items)
                .FirstOrDefaultAsync(i=>i.USerId == UserId);
            if (cart == null)
            {
              cart = new Cart { USerId = UserId };
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
            await _Context.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> RemoveFromCartAsync(int UserId, int ProductId )
        {
            var cart = await _Context.Carts.Include(c=>c.Items)
                .FirstOrDefaultAsync(i=>i.USerId == UserId);
            
            if (cart == null) throw new Exception("Cart not found");
            
            var item = cart.Items.FirstOrDefault(i => i.ProductId == ProductId);
            if (item == null) throw new Exception("Item not found");
            
           cart.Items.Remove(item);
           
            await _Context.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);

        }

        public async Task<CartDto> UpdateQuantityAsync(int UserId, int ProductId, int Quantity)
        {
            var cart = await _Context.Carts.Include(c=>c.Items)
                .FirstOrDefaultAsync(i=>i.USerId == UserId);
            
            if (cart == null) throw new Exception("Cart not found");
            
            var item = cart.Items.FirstOrDefault(i => i.ProductId == ProductId);
            if (item == null)  throw new Exception("Item not found");
            
            item.Quantity = Quantity;
            await _Context.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);
           
        }

        
    }