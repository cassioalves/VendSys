using Microsoft.AspNetCore.Mvc;
using VendSys.Services.Import.Interface;

namespace VendSys.Controllers
{
    [ApiController]
    [Route("vdi-dex")]
    public class DEXController : ControllerBase
    {
        private readonly IDEXImportService _importService;

        public DEXController(IDEXImportService importService)
        {
            _importService = importService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] IFormFile file, [FromForm] string machineName)
        {
            var result = await _importService.ImportAsync(file, machineName);

            return result.IsSuccess
                ? Ok(result.Payload)
                : BadRequest(result.Error);
        }
    }
}
