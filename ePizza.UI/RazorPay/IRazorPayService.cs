using Razorpay.Api;

namespace ePizza.UI.RazorPay
{
    public interface IRazorPayService
    {
        string CreateOrder(decimal amount, string currency, string receipt);

        Payment GetPayment(string paymentId);

        bool VerifySignature(string signature,string orderId, string paymentId);


        // create order
        // get payment
        // verify signature
    }
}
