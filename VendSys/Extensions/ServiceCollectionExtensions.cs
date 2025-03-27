using Microsoft.EntityFrameworkCore;
using VendSys.Data;
using VendSys.Models;
using VendSys.Services.Interface;
using VendSys.Services;
using VendSys.Services.Import.Interface;
using VendSys.Services.Import;

namespace VendSys.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVendSysServices(this IServiceCollection services, IConfiguration config)
    {
        // Register application services
        services.AddScoped<IDEXMeterService, DEXMeterService>();
        services.AddScoped<IDEXFileParser, DEXFileParser>();
        services.AddScoped<IDEXImportService, DEXImportService>();

        // Bind BasicAuth settings
        services.Configure<BasicAuthSettings>(
            config.GetSection("BasicAuth"));

        // Register EF DbContext
        services.AddDbContext<VdiDexDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        return services;
    }
}