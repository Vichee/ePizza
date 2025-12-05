namespace ePizza.Application.DTOs.Request
{
    public class UpdateCartItemRequestDto
    {
        public Guid CartId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
