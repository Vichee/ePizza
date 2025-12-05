namespace ePizza.Application.DTOs.Request
{
    public class UpdateCartUserDto
    {
        public Guid CartId { get; set; }
        public int UserId { get; set; }
    }
}
