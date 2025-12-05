using ePizza.UI.Constants;
using ePizza.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.UI.Controllers
{
    public class RegisterUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterUserController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel request)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.EPizzaApiClient);

                var registerUserRequest = await httpClient.PostAsJsonAsync("api/User/register-user", request);

                registerUserRequest.EnsureSuccessStatusCode();

                return RedirectToAction("Login", "Login");
            }

            return View(request);
        }
    }
}
