
using System.Linq.Expressions;

namespace ePizza.Domain.Interfaces
{
    public interface IGenericRepository<TDomain> where TDomain : class
    {
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task<TDomain> GetByIdAsync(object id);
        Task<IEnumerable<TDomain>> FindAsync(Expression<Func<TDomain, bool>> predicate);
        Task AddAsync(TDomain domainEntity);
        Task<int> CommitAsync();

        Task UpdateAsync(TDomain domainEntity, object id);

    }
}



// get records
// get record by id
// save records
// delete records
//update records