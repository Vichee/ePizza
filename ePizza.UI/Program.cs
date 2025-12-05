using ePizza.UI.Constants;
using ePizza.UI.RazorPay;
using ePizza.UI.TokenHelpers;
using ePizza.UI.Utils.Contract;
using ePizza.UI.Utils.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ePizza.UI
{

    // Cart UI 
    // login
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(options =>
                  {
                      options.LoginPath = "/Login/Login";
                      options.LogoutPath = "/Login/Logout";
  

                  });


            
            builder.Services.AddAuthorization();

            builder.Services.AddScoped<HttpInterceptor>();               

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IRazorPayService, RazorPayService>();

            builder.Services.AddHttpClient(ApplicationConstants.EPizzaApiClient, options =>
            {
                options.BaseAddress = new Uri(builder.Configuration["EPizzaAPI:baseUrl"]!);
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            })
             .AddHttpMessageHandler<HttpInterceptor>();
              



            //builder.Services.AddHttpClient("orderClient", options =>
            //{
            //    options.BaseAddress = new Uri(builder.Configuration["EPizzaAPI:baseUrl"]!); // order url
            //    options.DefaultRequestHeaders.Add("Accept", "application/json");
            //});

            //builder.Services.AddHttpClient("inventoryClient", options =>
            //{
            //    options.BaseAddress = new Uri(builder.Configuration["EPizzaAPI:baseUrl"]!); // inventory url
            //    options.DefaultRequestHeaders.Add("Accept", "application/json");
            //});


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
