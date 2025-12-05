using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;
using ePizza.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ePizza.Infrastructure.Repositories
{
    public class CartItemRepository : GenericRepository<CartItemDomain, CartItem>, ICartItemRepository
    {
        public CartItemRepository(
            EPizzaDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CartItemDomain> GetCartItemsAsync(Guid cartId, int itemId)
        {
            var cartItemDomains = await _context
                    .CartItems
                    .FirstOrDefaultAsync(
                            x => x.CartId == cartId && x.ItemId == itemId);

            return _mapper.Map<CartItemDomain>(cartItemDomains);    
        }
    }
}
