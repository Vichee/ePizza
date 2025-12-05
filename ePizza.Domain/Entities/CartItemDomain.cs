namespace ePizza.Domain.Entities
{
    public class CartItemDomain
    {
        public int Id { get; set; }
        public Guid CartId { get; set; }
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
    }
}
