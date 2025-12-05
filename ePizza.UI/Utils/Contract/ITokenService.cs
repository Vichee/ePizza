namespace ePizza.UI.Utils.Contract
{
    public interface ITokenService
    {
        void SetToken(string token);
        void SetRefreshToken(string refreshToken);
        string GetToken();

        Task GetRefreshTokenAsync(string accessToken, string refreshToken);
        DateTime? GetTokenExpiry(string accessToken);
    }
}
