using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;
using ePizza.Infrastructure.Entities;


namespace ePizza.Infrastructure.Repositories
{
    public class ItemRepository : GenericRepository<ItemDomain, Item>, IItemRepository
    {
        public ItemRepository(EPizzaDbContext context, IMapper mapper) : base(context,mapper)
        {
        }
    }
}
