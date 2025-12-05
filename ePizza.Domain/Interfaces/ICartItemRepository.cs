using ePizza.Domain.Entities;

namespace ePizza.Domain.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItemDomain>
    {
        Task<CartItemDomain> GetCartItemsAsync(Guid cartId, int itemId);
    }
}
