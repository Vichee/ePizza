using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Infrastructure.Entities;

namespace ePizza.Infrastructure.Mappers
{
    public class UserMappingExtension : Profile
    {
        public UserMappingExtension()
        {
            CreateMap<UserDomain, User>().ReverseMap();
        }
    }
}
