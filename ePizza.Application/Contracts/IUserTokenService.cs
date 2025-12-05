using ePizza.Application.DTOs.Request;

namespace ePizza.Application.Contracts
{
    public interface IUserTokenService
    {
        Task<int> PersistToken(UserTokenRequestDto userTokenRequest);
    }
}
