using ePizza.UI.Constants;
using ePizza.UI.Models.Response;
using ePizza.UI.Models.ViewModels;
using ePizza.UI.Utils.Contract;
using ePizza.UI.Utils.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ePizza.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ITokenService _tokenService;

        public LoginController(
            IHttpClientFactory httpClientFactory,
            ITokenService tokenService)
        {
            this.httpClientFactory = httpClientFactory;
            this._tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient(ApplicationConstants.EPizzaApiClient);

                var response =  await client.GetFromJsonAsync<ApiResponseModelDto<TokenResponseDto>>(
                    $"api/Token/{request.UserName}/{request.Password}");

                if (response is not null && response.Success)
                {
                    _tokenService.SetToken(response.Data.AccessToken);

                     var claims = await ProcessToken(response.Data.AccessToken);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(request);
        }

  
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }


        private async Task<List<Claim>> ProcessToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            var claims = new List<Claim>();

            foreach (var claim in jwtToken.Claims)
            {
                claims.Add(claim);
            }


            await GenerateTicket(claims);
            return claims;  
        }

        private async Task GenerateTicket(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties()
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                });
        }
    }
}
