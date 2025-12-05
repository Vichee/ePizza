using AutoMapper;
using ePizza.Application.DTOs.Response;
using ePizza.Domain.Entities;

namespace ePizza.Application.Mappers
{
    public class ItemServiceMappingExtension : Profile
    {
        public ItemServiceMappingExtension()
        {
            CreateMap<ItemDomain, ItemResponseDto>();
        }
    }
}
