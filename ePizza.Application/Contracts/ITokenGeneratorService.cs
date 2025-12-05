using ePizza.Application.DTOs.Request;
using ePizza.Application.DTOs.Response;

namespace ePizza.Application.Contracts
{
    public interface ITokenGeneratorService
    {
        Task<TokenResponseDto> GenerateTokenAsync(string userName, string password);

        Task<TokenResponseDto> GenerateRefreshTokenAsync(RefreshTokenRequestDto request);

    }
}
