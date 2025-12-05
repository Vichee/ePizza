using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;
using ePizza.Infrastructure.Entities;

namespace ePizza.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<OrderDomain, Order>, IOrderRepository
    {
        public OrderRepository(EPizzaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
