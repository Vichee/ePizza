namespace ePizza.UI.Models.Request
{
    public class AddToCartRequestDto
    {
        public Guid CartId { get; set; }
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
