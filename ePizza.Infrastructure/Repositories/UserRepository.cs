using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;
using ePizza.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ePizza.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<UserDomain, User>, IUserRepository
    {
        public UserRepository(EPizzaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<int> AddUserAsync(UserDomain userDomain)
        {
            var roles = await _context.Roles
                 .FirstAsync(x => x.Name == userDomain.UserRoles.First());

            var user = _mapper.Map<User>(userDomain);

            user.EmailConfirmed = true;// 
            user.CreatedDate = DateTime.UtcNow;

            user.Roles.Add(roles);
            _context.Users.Add(user);

            return await CommitAsync();

        }
    }
}
