using AutoMapper;
using ePizza.Application.Contracts;
using ePizza.Application.DTOs.Response;
using ePizza.Domain.Interfaces;

namespace ePizza.Application.Implementation
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository,
            IMapper mapper)
        {
            this._itemRepository = itemRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<ItemResponseDto>> GetItemsAsync()
        {
            var response = await _itemRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ItemResponseDto>>(response);
        }

        public async Task<ItemResponseDto> GetItemsByIdAsync(int id)
        {
            var response = await _itemRepository.GetByIdAsync(id);

            //var response2 = 
            //    await _itemRepository.FindAsync(
            //                    x => x.Description.Contains("Delightful") 
            //                    && x.Name.Contains("Farm")
            //                    && x.UnitPrice >= 299);

            return _mapper.Map<ItemResponseDto>(response);
        }
    }
}
