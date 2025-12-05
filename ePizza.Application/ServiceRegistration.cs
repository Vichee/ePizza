using ePizza.Application.Contracts;
using ePizza.Application.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace ePizza.Application
{
    public static  class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<IUserTokenService, UserTokenService>();
            services.AddScoped<IPaymentService, PaymentService>();


            return services;
        }

    }
}
