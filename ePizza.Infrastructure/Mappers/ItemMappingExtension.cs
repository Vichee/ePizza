using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Infrastructure.Entities;

namespace ePizza.Infrastructure.Mappers
{
    public class ItemMappingExtension : Profile
    {
        public ItemMappingExtension()
        {
            CreateMap<ItemDomain, Item>().ReverseMap();
        }
    }
}
