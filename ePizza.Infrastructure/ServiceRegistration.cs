using ePizza.Domain.Entities;
using ePizza.Domain.Interfaces;
using ePizza.Infrastructure.Entities;
using ePizza.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ePizza.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionString)
        {

            services.AddDbContext<EPizzaDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
                });


            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();



            return services;
        }
    }
}
