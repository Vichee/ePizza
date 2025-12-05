using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;

namespace ePizza.Infrastructure.Mappers
{
    public class UserTokenMappingExtension : Profile
    {
        public UserTokenMappingExtension()
        {
            CreateMap<UserTokenDomain, UserToken>()
                .ForMember(dest => dest.RefreshTokenExpiryTime,
                    opt
                        => opt.MapFrom<RefreshTokenExpiryResolver>());

            CreateMap<UserToken, UserTokenDomain>();

        }
    }

    public class RefreshTokenExpiryResolver :
        IValueResolver<UserTokenDomain, UserToken, DateTime>
    {
        private readonly IConfiguration _configuration;

        public RefreshTokenExpiryResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DateTime Resolve(
            UserTokenDomain source,
            UserToken destination,
            DateTime destMember,
            ResolutionContext context)
        {
            var raw = _configuration["Jwt:RefreshTokenExpiryInDays"];
            if (!int.TryParse(raw, out var expiryDays))
            {
                expiryDays = 7; // fallback default
            }

            return DateTime.UtcNow.AddDays(expiryDays);
        }
    }
}
