
using ePizza.UI.Utils.Contract;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;

namespace ePizza.UI.TokenHelpers
{
    public class HttpInterceptor : DelegatingHandler
    {
        private readonly ITokenService _tokenService;

        public HttpInterceptor(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {

            var accessToken = _tokenService.GetToken();

            if (!string.IsNullOrEmpty(accessToken))
            {
                var tokenExpireTime = _tokenService.GetTokenExpiry(accessToken)!;

                if (tokenExpireTime.Value <= DateTime.UtcNow.AddMinutes(2))
                {
                    await _tokenService.GetRefreshTokenAsync(accessToken, "");
                }
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }


            // 3. if not valid, then call new endpoint

            return await base.SendAsync(request, cancellationToken);
        }

    }
}
