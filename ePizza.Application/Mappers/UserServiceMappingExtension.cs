using AutoMapper;
using ePizza.Application.DTOs.Request;
using ePizza.Domain.Entities;

namespace ePizza.Application.Mappers
{
    public class UserServiceMappingExtension : Profile
    {
        public UserServiceMappingExtension()
        {
            CreateMap<RegisterUserDto, UserDomain>();
        }
    }
}
