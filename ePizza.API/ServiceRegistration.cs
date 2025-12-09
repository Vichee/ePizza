using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.Text;

namespace ePizza.API
{
    public static class ServiceRegistration
    {

        public static IServiceCollection RegisterJwt(this IServiceCollection services,
            IConfiguration configuration)
        {
            var secret = configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(secret))
                throw new InvalidOperationException("JWT Secret is missing in configuration.");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

    }
}
