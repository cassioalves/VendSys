using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using System.Text;
using VendSys.Models;

namespace VendSys.Middlewares;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly BasicAuthSettings _authSettings;

    public BasicAuthMiddleware(RequestDelegate next, IOptions<BasicAuthSettings> authOptions)
    {
        _next = next;
        _authSettings = authOptions.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Basic "))
        {
            var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
            var credentialBytes = Convert.FromBase64String(encodedCredentials);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

            if (credentials.Length == 2)
            {
                var username = credentials[0];
                var password = credentials[1];

                if (username == _authSettings.Username && password == _authSettings.Password)
                {
                    await _next(context);
                    return;
                }
            }
        }

        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    }
}