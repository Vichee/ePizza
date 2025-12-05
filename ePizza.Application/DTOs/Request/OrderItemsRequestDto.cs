

namespace ePizza.Application.DTOs.Request
{
    public class OrderItemsRequestDto
    {
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
