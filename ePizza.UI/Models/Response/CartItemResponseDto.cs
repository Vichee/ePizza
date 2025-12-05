namespace ePizza.UI.Models.Response
{
    public class CartItemResponseDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; } = default!;
        public string ItemName { get; set; } = default!;

        public decimal ItemTotal => Quantity * UnitPrice;
    }
}
