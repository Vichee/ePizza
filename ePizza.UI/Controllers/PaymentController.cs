using ePizza.UI.Constants;
using ePizza.UI.Models.Response;
using ePizza.UI.Models.ViewModels;
using ePizza.UI.RazorPay;
using ePizza.UI.Utils;
using Microsoft.AspNetCore.Mvc;
using ePizza.UI.Models.Request;
using Newtonsoft.Json;

// Serilog
// Deploying this in IIS
// SP using EFCore




// Health Check
// Exception Handling
// In Memory Cache
// API 
// User Token
// Make Payment

namespace ePizza.UI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IRazorPayService _razorPayService;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentController(IRazorPayService razorPayService,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            this._razorPayService = razorPayService;
            this._configuration = configuration;
            this._httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            PaymentViewModel viewModel = new PaymentViewModel();

            var cartDetails = TempData.Peek<CartResponseDto>("CartDetails");

            if (cartDetails != null)
            {
                viewModel.Currency = "INR";
                viewModel.RazorPayKey = _configuration["RazorPay:Key"]!;
                viewModel.GrantTotal = cartDetails.GrantTotal;
                viewModel.Cart = cartDetails;
                viewModel.Description = "Enjoy your meal";
                viewModel.Receipt = Guid.NewGuid().ToString();
                viewModel.OrderId = _razorPayService.CreateOrder(cartDetails.GrantTotal, "INR", viewModel.Receipt);
            }

            return View(viewModel);
        }



        public async Task<IActionResult> Status(IFormCollection form)
        {
            string paymentId = form["rzp_paymentid"]!;
            string orderId = form["rzp_orderid"]!;
            string signature = form["rzp_signature"]!;
            string transactionId = form["Receipt"]!;
            string currency = form["Currency"]!;

            bool isSignatureVerified = _razorPayService.VerifySignature(signature, orderId, paymentId);

            if (isSignatureVerified)
            {
                var paymentDetails = _razorPayService.GetPayment(paymentId);
                string status = paymentDetails["status"];


                var request = GetPaymentRequest(paymentId, orderId, transactionId, currency, status);


                var jsonRequest = JsonConvert.SerializeObject(request);

                var client = _httpClientFactory.CreateClient(ApplicationConstants.EPizzaApiClient);

                var response
                     = await client.PostAsJsonAsync("api/Payment", request);

                response.EnsureSuccessStatusCode();

                Response.Cookies.Delete("CartId");
                TempData.Remove("Address");
                TempData.Remove("CartDetails");


                return RedirectToAction("Receipt");

            }
            // payment api
            return View();
        }


        public IActionResult Receipt()
        {
            return View();
        }

        private MakePaymentRequestDto GetPaymentRequest(
         string paymentId, string orderid, string transactionid, string currency, string status)
        {
            CartResponseDto cart = TempData.Peek<CartResponseDto>("CartDetails");

            AddressViewModel addressViewModel = TempData.Peek<AddressViewModel>("Address");


            return new MakePaymentRequestDto
            {
                CartId = Guid.Parse(Request.Cookies["CartId"])!,
                Total = cart.Total,
                Currency = currency,
                PaymentId = paymentId,
                Status = status,
                TransactionId = transactionid,
                Tax = cart.Tax,
                Email = CurrentUser.Email,
                GrandTotal = cart.GrantTotal,
                UserId = CurrentUser.UserId,
                OrderRequest = new OrderRequestModelDto()
                {
                    City = addressViewModel.City,
                    Locality = addressViewModel.Locality,
                    Street = addressViewModel.Street,
                    UserId = CurrentUser.UserId,
                    OrderId = orderid,
                    PaymentId = paymentId,
                    PhoneNumber = addressViewModel.PhoneNumber,
                    ZipCode = addressViewModel.ZipCode,
                    OrderItems = GetOrderItems(cart.CartItems)
                }
            };
        }

        private List<OrderItemsRequestDto> GetOrderItems(List<CartItemResponseDto> items)
        {
            List<OrderItemsRequestDto> orderItems = [];

            items.ForEach(x => orderItems.Add(new OrderItemsRequestDto()
            {
                ItemId = x.ItemId,
                Quantity = x.Quantity,
                Total = x.ItemTotal,
                UnitPrice = x.UnitPrice
            }));


            return orderItems;
        }
    }
}
