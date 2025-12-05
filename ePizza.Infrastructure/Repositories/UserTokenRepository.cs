using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace ePizza.Infrastructure.Repositories
{
    public class UserTokenRepository : GenericRepository<UserTokenDomain, UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(EPizzaDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<UserTokenDomain> GetUserTokenAsync(int userId)
        {
            var userToken
                = await _context.UserTokens.FirstOrDefaultAsync(x => x.UserId == userId);

            return _mapper.Map<UserTokenDomain>(userToken);
        }

        public async Task<int> AddUserTokenAsync(UserTokenDomain userTokenDomain)
        {
            var tokenDetails
                = await _context.UserTokens.Where(x => x.UserId == userTokenDomain.UserId).ToListAsync();

            if (tokenDetails.Any())
                _context.UserTokens.RemoveRange(tokenDetails);

            await _context.AddAsync(_mapper.Map<UserToken>(userTokenDomain));

            return await _context.SaveChangesAsync();
        }
    }
}
