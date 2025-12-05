using ePizza.Application.Contracts;
using ePizza.Application.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentSevice;

        public PaymentController(IPaymentService paymentSevice)
        {
            this._paymentSevice = paymentSevice;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MakePaymentRequestDto request)
        {
            var respone = await _paymentSevice.CapturePaymentDetailsAsync(request);
            return Ok(respone);
        }
    }
}
