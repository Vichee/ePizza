

namespace ePizza.Domain.Entities
{
    public class UserTokenDomain
    {
        public string AccessToken { get; set; } = default!;

        public string RefreshToken { get; set; } = default!;

        public int UserId { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
