using ePizza.UI.Constants;
using ePizza.UI.Models.Request;
using ePizza.UI.Models.Response;
using ePizza.UI.Models.ViewModels;
using ePizza.UI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace ePizza.UI.Controllers
{
    [Route("Cart")]
    public class CartController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CartController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        Guid CartId
        {

            get
            {
                Guid id;
                string cartId = Request.Cookies[ApplicationConstants.CartId]!;

                if (cartId == null)
                {
                    id = Guid.NewGuid();

                    Response.Cookies.Append(ApplicationConstants.CartId, id.ToString(), new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(10),
                        Secure = true

                    });
                }
                else
                {
                    id = Guid.Parse(cartId);
                }

                return id;
            }
        }


        public async Task<IActionResult> Index()
        {

            using var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.EPizzaApiClient);

            var cartItems = await httpClient
                .GetFromJsonAsync<ApiResponseModelDto<CartResponseDto>>($"api/Cart/get-cart-detail?cartId={CartId}");

            return View(cartItems!.Data);
        }


        // add item to cart

        [HttpGet("AddToCart/{itemId:int}/{unitPrice:decimal}/{quantity:int}")]
        public async Task<JsonResult> AddItemToCart(int itemId, decimal unitPrice, int quantity)
        {
            
            using var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.EPizzaApiClient);

            var cartRequest = new AddToCartRequestDto
            {
                CartId = CartId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                ItemId = itemId
            };
            var addItemResponse
                 = await httpClient.PostAsJsonAsync("api/Cart/add-items", cartRequest);

            addItemResponse.EnsureSuccessStatusCode();
            var itemCount = await GetItemCount(CartId);
            return Json(new { Count = itemCount });
        }


        [HttpGet("GetCartCount")]
        public async Task<JsonResult> GetCartItemCount()
        {
            var itemCount = await GetItemCount(CartId);

            return Json(new { Count = itemCount });
        }


        [HttpGet("Checkout")]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View();  
        }

        [HttpPost("Checkout")]
        [Authorize]
        public async Task<IActionResult> Checkout(AddressViewModel request)
        {
            if(ModelState.IsValid && CurrentUser != null)
            {

                using var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.EPizzaApiClient);

                var cartItems = await httpClient.GetFromJsonAsync<ApiResponseModelDto<CartResponseDto>>($"api/Cart/get-cart-detail?cartId={CartId}");

                var updateCartRequest = new
                {
                    CartId = CartId,
                    UserId = CurrentUser.UserId
                };

                
                var updateUserResponse = await httpClient.PutAsJsonAsync("api/Cart/update-cart-user", updateCartRequest);
                updateUserResponse.EnsureSuccessStatusCode();

                TempData.Set("Address", request);
                TempData.Set("CartDetails", cartItems!.Data);


                return RedirectToAction("Index", "Payment");


            }
            return View(request);
        }


        [NonAction]
        public async Task<int> GetItemCount(Guid CartId)
        {
            using var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.EPizzaApiClient);

            var itemCount = await httpClient.GetFromJsonAsync<ApiResponseModelDto<int>>
                ($"api/Cart/get-item-count?cartId={CartId}");

            if (itemCount != null) return itemCount.Data;

            return await Task.FromResult(0);
        }
    }
}
