

namespace ePizza.Application.DTOs.Response
{
    public class ItemResponseDto
    {
        public int Id { get; set; }  

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; } = default!;
    }
}
