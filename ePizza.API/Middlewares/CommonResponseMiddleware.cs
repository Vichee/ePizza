using ePizza.Application.DTOs.Response;
using System.Text.Json;

namespace ePizza.API.Middlewares;


public class CommonResponseMiddleware
{
    private readonly RequestDelegate _next;

    public CommonResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using var memoryStream = new MemoryStream();
        var originalBody = context.Response.Body;
        try
        {
            context.Response.Body = memoryStream;
            await _next(context);
        }
        catch (Exception ex)
        {
            // log it where
            throw;
        }

        if (context.Response.ContentType != null
            && context.Response.ContentType.Contains("application/json"))
        {
            memoryStream.Seek(0, SeekOrigin.Begin);

            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

            var responseObj = new ApiResponseModelDto<object>(
                    success: context.Response.StatusCode is >= 200 and <= 299,
                    data: JsonSerializer.Deserialize<object>(responseBody)!,
                    message: "Request completed"
                );

            var jsonResponse = JsonSerializer.Serialize(responseObj);
            context.Response.Body = originalBody;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
