using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;
using ePizza.Application.Contracts;
using ePizza.Application.DTOs.Request;
using ePizza.Application.DTOs.Response;

namespace ePizza.Application.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public CartService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddItemsAsync(AddItemsDto request)
        {
            var cartDetails = await _cartRepository.GetByIdAsync(request.CartId);
            int recordsInserted = 0;

            if (cartDetails == null)
            {
                await _cartRepository.AddAsync(GetCartDomain(request));
                await _cartItemRepository.AddAsync(GetCartItemDomain(request));

                recordsInserted = await _cartItemRepository.CommitAsync();
            }
            else
            {
                recordsInserted = await AddItemsToCart(request, cartDetails);
            }

            return recordsInserted > 0;
        }

        public async Task<int> GetItemCountAsync(Guid cartId)
        {
            return await _cartRepository.GetCartItemsCountAsnc(cartId);
        }

        public async Task<CartResponseDto> GetCartDetailsAsync(Guid cartId)
        {
            var cartItems = await _cartRepository.GetCartDetailAsync(cartId);
            cartItems.CalculateTotal();

            return _mapper.Map<CartResponseDto>(cartItems);
        }

        public async Task<int> UpdateCartUserAsync(
            UpdateCartUserDto request)
        {
            return await _cartRepository.UpdateCartUserAsync(request.CartId,request.UserId);
        }

        private CartDomain GetCartDomain(AddItemsDto request) => _mapper.Map<CartDomain>(request);

        private CartItemDomain GetCartItemDomain(AddItemsDto request) => _mapper.Map<CartItemDomain>(request);

        private async Task<int> AddItemsToCart(AddItemsDto itemsDto, CartDomain cartDomain)
        {
            CartItemDomain cartItemDomain =
                await _cartItemRepository.GetCartItemsAsync(itemsDto.CartId, itemsDto.ItemId);

            if (cartItemDomain is not null)
            {
                cartItemDomain.Quantity += itemsDto.Quantity;

                await _cartItemRepository.UpdateAsync(cartItemDomain, cartItemDomain.Id);
            }
            else
            {
                await _cartItemRepository.AddAsync(GetCartItemDomain(itemsDto));
            }

            return await _cartItemRepository.CommitAsync();
        }

        public async Task<bool> DeleteItemFromCartAsync(Guid cartId, int itemId)
        {
            var isdeleted = await _cartRepository.DeleteItemFromCartAsync(cartId, itemId);

            return isdeleted;
        }

        public async Task<bool> UpdateItemInCartAsync(Guid cartId, int itemId, int quantity)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);

            int recordsUpdated = await _cartRepository.UpdateItemQuantity(cartId, itemId, quantity);
            return recordsUpdated > 0;
        }
    }
}