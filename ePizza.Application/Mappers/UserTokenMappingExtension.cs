using AutoMapper;
using ePizza.Application.DTOs.Request;
using ePizza.Application.DTOs.Response;
using ePizza.Domain.Entities;


namespace ePizza.Application.Mappers
{
    public class UserTokenMappingExtension : Profile
    {
        public UserTokenMappingExtension()
        {
            CreateMap<UserTokenRequestDto, UserTokenDomain>();

            CreateMap<UserTokenDomain, RefreshTokenResponseDto>();
        }
    }
}
