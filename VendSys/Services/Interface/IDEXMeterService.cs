using VendSys.Models;

namespace VendSys.Services.Interface
{
    public interface IDEXMeterService
    {
        Task<DEXMeter> SaveAsync(DEXMeter dexMeter);
        Task<DEXLaneMeter> SaveLaneAsync(DEXLaneMeter lane);
    }
}
