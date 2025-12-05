using ePizza.Domain.Interfaces;

namespace ePizza.Domain.Entities
{
    public interface IUserTokenRepository : IGenericRepository<UserTokenDomain>
    {
        Task<UserTokenDomain> GetUserTokenAsync(int userId);

        Task<int> AddUserTokenAsync(UserTokenDomain userTokenDomain);

    }
}
