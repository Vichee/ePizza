using ePizza.UI.Utils.Contract;
using System.IdentityModel.Tokens.Jwt;

namespace ePizza.UI.Utils.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContext;

        public TokenService(IHttpContextAccessor httpContext)
        {
            this._httpContext = httpContext;
        }

        public Task GetRefreshTokenAsync(string accessToken, string refreshToken)
        {
           return Task.CompletedTask;
        }

        public string GetToken()
        {
            var context = _httpContext.HttpContext;
            if (context == null)
                return string.Empty;

            if(context.Request.Cookies.TryGetValue("access-token", out var token))
                return token;

            return string.Empty;
        }

        public DateTime? GetTokenExpiry(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(accessToken);

            var expiry = token.Payload.Expiration;

            if (expiry.HasValue)
            {
                var tokenExpiry = DateTimeOffset.FromUnixTimeSeconds(expiry.Value).UtcDateTime;
                return tokenExpiry;
            }

            return null;
        }

        public void SetRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public void SetToken(string token)
        {
            var context = _httpContext.HttpContext;
            if (context == null)
                return;

            context.Response.Cookies.Append("access-token", token, new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(10)
            });
        }
    }
}
