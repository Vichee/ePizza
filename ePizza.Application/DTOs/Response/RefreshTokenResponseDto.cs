
namespace ePizza.Application.DTOs.Response
{
    public class RefreshTokenResponseDto
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
