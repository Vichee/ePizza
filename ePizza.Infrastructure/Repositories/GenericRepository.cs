using AutoMapper;
using AutoMapper.QueryableExtensions;
using ePizza.Domain.Interfaces;
using ePizza.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace ePizza.Infrastructure.Repositories
{
    public class GenericRepository<TDomain, TEntity> : IGenericRepository<TDomain>
        where TDomain : class
        where TEntity : class
    {
        protected readonly EPizzaDbContext _context;
        protected readonly IMapper _mapper;

        public GenericRepository(EPizzaDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TDomain>> FindAsync(Expression<Func<TDomain, bool>> predicate)
        {

            var response = await _context.Set<TEntity>()
                .ProjectTo<TDomain>(_mapper.ConfigurationProvider)
                .Where(predicate)
                .ToListAsync();

            return response;
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            return await _context
                        .Set<TEntity>()
                        .ProjectTo<TDomain>(_mapper.ConfigurationProvider)
                        .ToListAsync();

        }

        public async Task<TDomain> GetByIdAsync(object id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            return entity == null ? null : _mapper.Map<TDomain>(entity);

        }

        public async Task AddAsync(TDomain domainEntity)
        {
            var entity = _mapper.Map<TEntity>(domainEntity);

            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task UpdateAsync(TDomain domainEntity, object id)
        {
            var existingEntity = await _context.Set<TEntity>().FindAsync(id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");

            _mapper.Map(domainEntity, existingEntity);
        }

    }
}
