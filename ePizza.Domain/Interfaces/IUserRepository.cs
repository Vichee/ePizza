
using ePizza.Domain.Entities;

namespace ePizza.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserDomain>
    {

        Task<int> AddUserAsync(UserDomain userDomain);    
    }
}
