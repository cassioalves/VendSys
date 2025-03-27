using VendSys.Middlewares;

namespace VendSys.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseVendSysMiddleware(this IApplicationBuilder app)
    {
        // Register custom middlewares
        app.UseMiddleware<BasicAuthMiddleware>();
        return app;
    }
}