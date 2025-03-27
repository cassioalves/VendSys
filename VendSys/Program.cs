using VendSys.Extensions;

namespace VendSys
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddVendSysServices(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // Register middleware for Basic Auth before authorization middleware
            app.UseVendSysMiddleware();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}