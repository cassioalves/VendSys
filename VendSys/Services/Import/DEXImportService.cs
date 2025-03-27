using VendSys.Services.Import.Interface;
using VendSys.Services.Interface;

namespace VendSys.Services.Import
{
    public class DEXImportService : IDEXImportService
    {
        private readonly IDEXMeterService _dexMeterService;
        private readonly IDEXFileParser _fileParser;

        public DEXImportService(IDEXMeterService dexMeterService, IDEXFileParser fileParser)
        {
            _dexMeterService = dexMeterService;
            _fileParser = fileParser;
        }

        public async Task<DEXImportResult> ImportAsync(IFormFile file, string machineName)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return DEXImportResult.Fail("Invalid file.");

                var ext = Path.GetExtension(file.FileName).ToLower();
                if (ext != ".txt")
                    return DEXImportResult.Fail("Only .txt files are allowed.");

                if (file.ContentType != "text/plain")
                    return DEXImportResult.Fail("Invalid MIME type. Expected: text/plain.");

                // Parse main meter data from the uploaded file
                var meter = await _fileParser.ParseDEXMeterAsync(machineName, file);

                // Persist meter to the database
                var savedMeter = await _dexMeterService.SaveAsync(meter);

                // Parse associated lane meters from the same file
                var lanes = await _fileParser.ParseDEXLaneMeterAsync(savedMeter.Id, file);

                // Save lane meters concurrently to improve performance
                var options = new ParallelOptions { MaxDegreeOfParallelism = 4 };
                await Parallel.ForEachAsync(lanes, options, async (lane, _) =>
                {
                    await _dexMeterService.SaveLaneAsync(lane);
                });

                return DEXImportResult.Success(new
                {
                    savedMeter.Id,
                    file.FileName,
                    file.ContentType,
                    file.Length
                });
            }
            catch (Exception ex)
            {
                return DEXImportResult.Fail($"An error occurred while processing the file. Error - {ex}");
            }
        }
    }
}