namespace ePizza.Application.DTOs.Response
{
    public class CartItemsResponseDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; } = default!;
        public string ItemName { get; set; } = default!;
    }
}
