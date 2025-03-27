using VendSys.Models;

namespace VendSys.Services.Interface
{
    public interface IDEXFileParser
    {
        Task<DEXMeter> ParseDEXMeterAsync(string machineName, IFormFile file);
        Task<List<DEXLaneMeter>> ParseDEXLaneMeterAsync(int dexMeterId, IFormFile file);
    }
}
