using ePizza.UI.Models.Response;

namespace ePizza.UI.Models.ViewModels
{
    public class PaymentViewModel
    {
        public string Receipt { get; set; } = default!;

        public string Currency {  get; set; } = default!;

        public CartResponseDto Cart { get; set; } = new();

        public string RazorPayKey { get; set; } = default!;

        public decimal GrantTotal {  get; set; }= default!;

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string OrderId { get; set; } = default!;

    }
}
