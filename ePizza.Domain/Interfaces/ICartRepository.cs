using ePizza.Domain.Entities;

namespace ePizza.Domain.Interfaces
{
    public interface ICartRepository : IGenericRepository<CartDomain>
    {
        Task<int> GetCartItemsCountAsnc(Guid cartId);
        Task<CartDomain> GetCartDetailAsync(Guid cartId);   
        Task<int> UpdateCartUserAsync(Guid cartId, int userId);
        Task<bool> DeleteItemFromCartAsync(Guid cartId, int itemId);
        Task<int> UpdateItemQuantity(Guid cartId, int itemId, int quantity);
    }
}
