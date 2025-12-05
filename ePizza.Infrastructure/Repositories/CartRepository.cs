using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;
using ePizza.Infrastructure.Entities;
using ePizza.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ePizza.Infrastructure.Repositories
{
    public class CartRepository : GenericRepository<CartDomain, Cart>, ICartRepository
    {
        public CartRepository(
            EPizzaDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<bool> DeleteItemFromCartAsync(Guid cartId, int itemId)
        {
            var cartItems = await _context.CartItems.FirstOrDefaultAsync(x => x.CartId == cartId && x.ItemId == itemId);

            if(cartItems != null) {
                _context.CartItems.Remove(cartItems);

                return await CommitAsync() > 0;
            }

            return false;
        }

        public async Task<CartDomain> GetCartDetailAsync(Guid cartId)
        {
            var result = await _context.Carts
                .Where(x => x.Id == cartId && x.IsActive)
                .Include(x => x.CartItems)
                .ThenInclude(ci => ci.Item)
                .FirstOrDefaultAsync();

            return result.ToDomain();
        }

        public async Task<int> GetCartItemsCountAsnc(Guid cartId)
        {
            var itemCount = await _context.CartItems.Where(x => x.CartId == cartId).CountAsync();
            return itemCount;
        }

        public async Task<int> UpdateCartUserAsync(Guid cartId, int userId)
        {
            var cart =
                await _context.Carts.FirstOrDefaultAsync(x => x.Id == cartId);

            if (cart is not null)
                cart.UserId = userId;

            return await CommitAsync();
        }

        public async Task<int> UpdateItemQuantity(Guid cartId, int itemId, int quantity)
        {
            var currentItems = await _context
                                            .CartItems
                                                .Where(x => x.CartId == cartId
                                                       && x.ItemId == itemId)
                                                .FirstOrDefaultAsync();

            currentItems.Quantity = quantity;
            _context.Entry(currentItems).State = EntityState.Modified;
            return await CommitAsync();
        }
    }
}