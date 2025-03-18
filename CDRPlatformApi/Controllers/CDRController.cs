using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using CDRPlatform.Domain.Interfaces.Services;

namespace CDRPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDRController : ControllerBase
    {
        private readonly ILogger<CDRController> _logger;
        private readonly ICallDetailRecordService _callDetailRecordService;

        public CDRController(ILogger<CDRController> logger, ICallDetailRecordService callDetailRecordService)
        {
            _logger = logger;
            _callDetailRecordService = callDetailRecordService;

        }
        [HttpPost("import-cdrs-from-csv")]
        public async Task<IActionResult> ImportCallDetailRecordsFromCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded or it's empty");

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    await _callDetailRecordService.AddCallDetailRecordsAsync(stream);
                    return Ok(new { success = true, message = "Import completed successfully" });
                }
            }
            catch (CsvHelperException ex)
            {
                _logger.LogError(ex, "CSV parsing error");
                return StatusCode(400, $"Error parsing CSV on row {ex?.Context?.Parser?.Row}: {ex?.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing CSV");
                return StatusCode(500, $"Error importing CSV: {ex.Message}");
            }
        }
    }
}
