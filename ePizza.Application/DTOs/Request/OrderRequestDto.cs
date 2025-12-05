

namespace ePizza.Application.DTOs.Request
{
    public class OrderRequestDto
    {
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderItemsRequestDto> OrderItems { get; set; } = [];
    }
}
