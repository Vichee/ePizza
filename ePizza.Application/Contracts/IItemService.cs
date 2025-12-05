
using ePizza.Application.DTOs.Response;

namespace ePizza.Application.Contracts
{
    public interface IItemService
    {
        Task<IEnumerable<ItemResponseDto>> GetItemsAsync();

        Task<ItemResponseDto> GetItemsByIdAsync(int id);
    }
}
