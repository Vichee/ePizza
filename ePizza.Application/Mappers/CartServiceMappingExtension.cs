using AutoMapper;
using ePizza.Application.DTOs.Request;
using ePizza.Application.DTOs.Response;
using ePizza.Domain.Entities;


namespace ePizza.Application.Mappers
{
    public class CartServiceMappingExtension : Profile
    {
        public CartServiceMappingExtension()
        {
            CreateMap<AddItemsDto, CartItemDomain>();
            CreateMap<AddItemsDto, CartDomain>()
                .ForMember(dest => dest.Id,
                    opt
                        => opt.MapFrom(src => src.CartId));

            CreateMap<CartDomain, CartResponseDto>()
                .ForMember(dest => dest.CartId,
                    opt
                            => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CartItems,
                    opt
                        => opt.MapFrom(src => src.ItemDomains));

            CreateMap<CartItemDomain, CartItemsResponseDto>();
        }
    }
}
