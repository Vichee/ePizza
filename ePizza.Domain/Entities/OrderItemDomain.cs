
namespace ePizza.Domain.Entities
{
    public class OrderItemDomain
    {
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }

    }
}
