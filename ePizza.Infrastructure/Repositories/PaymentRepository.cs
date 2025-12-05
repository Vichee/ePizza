using AutoMapper;
using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;
using ePizza.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<PaymentDomain, PaymentDetail>, IPaymentRepository
    {
        public PaymentRepository(EPizzaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
