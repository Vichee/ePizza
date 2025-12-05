namespace ePizza.Domain.Entities
{
    public class CartDomain
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public List<CartItemDomain> ItemDomains { get; set; } = new();

        public decimal Total { get; private set; }
        public decimal Tax { get; private set; }
        public decimal GrantTotal { get; private set; }

        public void CalculateTotal()
        {
            Total= ItemDomains.Sum(x => x.UnitPrice * x.Quantity);

            Tax = (0.05m * Total);  // tax is 5%, can be configured somewhere else as well
            
            GrantTotal = Total + Tax;
        }
    }
}
