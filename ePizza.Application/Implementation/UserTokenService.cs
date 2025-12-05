using AutoMapper;
using ePizza.Application.Contracts;
using ePizza.Application.DTOs.Request;
using ePizza.Domain.Entities;

namespace ePizza.Application.Implementation
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IMapper _mapper;
        private readonly IUserTokenRepository _userTokenRepository;

        public UserTokenService(IMapper mapper,
            IUserTokenRepository userTokenRepository)
        {
            this._mapper = mapper;
            this._userTokenRepository = userTokenRepository;
        }

        public async Task<int> PersistToken(UserTokenRequestDto userTokenRequest)
        {
            var userTokenDomain = _mapper.Map<UserTokenDomain>(userTokenRequest);

            return await _userTokenRepository.AddUserTokenAsync(userTokenDomain);
        }
    }
}
