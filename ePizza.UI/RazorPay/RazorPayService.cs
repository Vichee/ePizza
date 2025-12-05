using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace ePizza.UI.RazorPay
{
    public class RazorPayService : IRazorPayService
    {
        private readonly IConfiguration _configuration;
        private readonly RazorpayClient _razorPayClient;

        public RazorPayService(IConfiguration configuration)
        {
            _configuration = configuration;
            _razorPayClient = new RazorpayClient(_configuration["RazorPay:Key"], _configuration["RazorPay:Secret"]);
        }

        public string CreateOrder(decimal amount, string currency, string receipt)
        {
            Dictionary<string, object> data
                 = new Dictionary<string, object>
                 {
                     {"amount", Convert.ToInt32(amount) },
                     { "receipt",receipt},
                     {"currency",currency }
                 };
            Order order = _razorPayClient.Order.Create(data);

            return order["id"].ToString();  
        }

        public Payment GetPayment(string paymentId)
        {
            return _razorPayClient.Payment.Fetch(paymentId);
        }

        public bool VerifySignature(string signature, string orderId, string paymentId)
        {
            string payload = string.Format("{0}|{1}", orderId, paymentId);
            string secret = RazorpayClient.Secret;
            string actualSignature = getActualSignature(payload, secret);
            return actualSignature.Equals(signature);
        }

        private static string getActualSignature(string payload, string secret)
        {
            byte[] secretBytes = StringEncode(secret);
            HMACSHA256 hashHmac = new HMACSHA256(secretBytes);
            var bytes = StringEncode(payload);

            return HashEncode(hashHmac.ComputeHash(bytes));
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
