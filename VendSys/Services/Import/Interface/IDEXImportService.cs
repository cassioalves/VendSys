namespace VendSys.Services.Import.Interface
{
    public interface IDEXImportService
    {
        Task<DEXImportResult> ImportAsync(IFormFile file, string machineName);
    }
}
