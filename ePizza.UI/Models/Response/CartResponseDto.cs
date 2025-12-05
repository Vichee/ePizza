namespace ePizza.UI.Models.Response
{
    public class CartResponseDto
    {
        public Guid CartId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal GrantTotal { get; set; }
        public List<CartItemResponseDto> CartItems { get; set; } = [];
    }
}
