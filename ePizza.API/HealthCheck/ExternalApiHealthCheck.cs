using Azure;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ePizza.API.HealthCheck
{
    public class ExternalApiHealthCheck : IHealthCheck
    {
        private readonly HttpClient client;

        public ExternalApiHealthCheck(HttpClient client)
        {
            this.client = client;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            try
            {
                var response = await client.GetAsync("https://reqres.in/api/users?page=2");

                if (response.IsSuccessStatusCode)
                    return HealthCheckResult.Healthy("Regres API is running");

                return HealthCheckResult.Degraded($"Regres API returned status : {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Regres API unreachable : {ex.Message}");
            }
        }
    }

    public class SecondApiHealthCheck : IHealthCheck
    {
        private readonly HttpClient client;

        public SecondApiHealthCheck(HttpClient client)
        {
            this.client = client;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(

            HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            try
            {
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");

                if (response.IsSuccessStatusCode)
                    return HealthCheckResult.Healthy("Second API is running");

                return HealthCheckResult.Degraded($"Second API returned status : {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Second API unreachable : {ex.Message}");
            }
        }
    }
}
