
namespace ePizza.Domain.Entities
{
    public class ItemDomain
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; } = default!;

        public DateTime CreatedDate { get; set; }
    }
}
