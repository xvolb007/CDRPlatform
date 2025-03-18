using CDRPlatform.AppServices.Interfaces;
using CDRPlatform.Domain.Models;
using CDRPlatform.Domain.Interfaces.Repositories;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;

namespace CDRPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDRController : ControllerBase
    {
        private readonly ICsvImportService _csvImportService;
        private readonly ILogger<CDRController> _logger;
        private readonly ICallDetailRecordRepository _callDetailRecordRepository;
        public CDRController(ICsvImportService csvImportService, ILogger<CDRController> logger, ICallDetailRecordRepository callDetailRecordRepository)
        {
            _csvImportService = csvImportService;
            _logger = logger;
            _callDetailRecordRepository = callDetailRecordRepository;

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
                    var records = await _csvImportService.ReadCSV<CallDetailRecord>(stream);
                    await _callDetailRecordRepository.AddCallDetailRecordsAsync(records);
                    return Ok(new { success = true, count = records.Count });
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
