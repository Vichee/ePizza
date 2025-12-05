using ePizza.Application.DTOs.Request;

namespace ePizza.Application.Contracts
{
    public interface IPaymentService
    {
        Task<string> CapturePaymentDetailsAsync(MakePaymentRequestDto request);
    }
}
