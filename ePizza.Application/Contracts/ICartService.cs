using ePizza.Application.DTOs.Request;
using ePizza.Application.DTOs.Response;

namespace ePizza.Application.Contracts
{
    public interface ICartService
    {
        Task<int> GetItemCountAsync(Guid cartId);
        Task<bool> AddItemsAsync(AddItemsDto request);
        Task<CartResponseDto> GetCartDetailsAsync(Guid cartId);
        Task<int> UpdateCartUserAsync(UpdateCartUserDto request);
        Task<bool> DeleteItemFromCartAsync(Guid cartId, int itemId);
        Task<bool> UpdateItemInCartAsync(Guid cartId, int itemId, int quantity);
    }
}
