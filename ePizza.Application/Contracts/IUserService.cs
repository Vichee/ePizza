
using ePizza.Application.DTOs.Request;
using ePizza.Domain.Entities;

namespace ePizza.Application.Contracts
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto register);
        Task<UserTokenDomain> GetUserTokenAsync(int userId);
        Task<UserDomain> GetUserAsync(string username);
    }
}
