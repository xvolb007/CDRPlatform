using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using CDRPlatform.Domain.Interfaces.Services;
using CDRPlatform.Domain.Models;
using CDRPlatform.Domain.Dto.CallDetailRecordDtos;

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
        /// <summary>
        /// Imports call detail records from a CSV file
        /// </summary>
        /// <param name="file">CSV file containing call detail records data</param>
        /// <remarks>
        /// The CSV file should contain headers and data in the following format:
        /// CallerId,CallerNumber,DestinationNumber,CallDate,CallDuration,CallCost
        /// File size should not exceed 30MB.
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
                _logger.LogError(ex, $"{nameof(CDRController)} error occurred at {nameof(ImportCallDetailRecordsFromCsv)} action.");
                return StatusCode(500, $"Error importing CSV: {ex.Message}");
            }
        }
        /// <summary>
        /// Gets all call records for a specific caller ID
        /// </summary>
        /// <param name="callerId">The unique identifier of the caller</param>
        [HttpGet("calls-by-caller-id")]
        public async Task<IActionResult> GetCallsByCallerId([FromQuery] long callerId)
        {
            try
            {
                IEnumerable<CallDetailRecordDto> callsByCallerId = await _callDetailRecordService.GetCallsByCallerIdAsync(callerId);
                return Ok(callsByCallerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CDRController)} error occurred at {nameof(GetCallsByCallerId)} action.");
                return StatusCode(500, $"Error retrieving calls by caller ID: {ex.Message}");
            }
        }
        /// <summary>
        /// Gets a paginated list of call records
        /// </summary>
        /// <param name="page">Page number (1-based)</param>
        /// <param name="pageSize">Number of records per page (default: 20, max: 100)</param>
        [HttpGet("calls-by-page")]
        public async Task<IActionResult> GetCallsByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                if (page < 1)
                {
                    return BadRequest("Page number must be greater than 0");
                }

                if (pageSize < 1 || pageSize > 100)
                {
                    return BadRequest("Page size must be between 1 and 100");
                }

                var callRecords = await _callDetailRecordService.GetCallsByPageAsync(page, pageSize);

                if (callRecords == null || !callRecords.Any())
                {
                    return NotFound("No call records found for the specified page");
                }

                return Ok(callRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CDRController)} error occurred at {nameof(GetCallsByPage)} action.");
                return StatusCode(500, $"Error retrieving call records by page: {ex.Message}");
            }
        }
        /// <summary>
        /// Retrieves the average cost of all calls in the system
        /// </summary>
        /// <returns>Average call cost in the system's currency</returns>
        [HttpGet("average-call-cost")]
        public async Task<IActionResult> GetAverageCallCost()
        {
            try
            {
                decimal averageCallCost = await _callDetailRecordService.GetAverageCallCostAsync();
                return Ok(new { averageCallCost });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CDRController)} error occurred at {nameof(GetAverageCallCost)} action.");
                return StatusCode(500, $"Error retrieving average call cost: {ex.Message}");
            }
        }
        /// <summary>
        /// Gets the duration of the longest call recorded in the system
        /// </summary>
        /// <returns>Duration of the longest call as TimeSpan</returns>
        [HttpGet("longest-call-duration")]
        public async Task<IActionResult> GetLongestCallDuration()
        {
            try
            {
                TimeSpan longestCallDuration = await _callDetailRecordService.GetLongestCallDurationAsync();
                return Ok(new { longestCallDuration });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CDRController)} error occurred at {nameof(GetLongestCallDuration)} action.");
                return StatusCode(500, $"Error retrieving longest call duration: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the total count of all calls in the system
        /// </summary>
        /// <returns>Total number of call records</returns>
        [HttpGet("total-calls-count")]
        public async Task<IActionResult> GetTotalCallsCount()
        {
            try
            {
                int totalCallsCount = await _callDetailRecordService.GetTotalCallsCountAsync();
                return Ok(new { totalCallsCount });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CDRController)} error occurred at {nameof(GetTotalCallsCount)} action.");
                return StatusCode(500, $"Error retrieving total calls count: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the total number of calls within a specified time period
        /// </summary>
        /// <param name="startDate">The beginning of the period</param>
        /// <param name="endDate">The end of the period</param>
        /// <returns>Number of calls that occurred during the specified period</returns>
        [HttpGet("total-calls-count-in-period")]
        public async Task<IActionResult> GetTotalCallsCountInPeriod([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            try
            {
                if (startDate >= endDate)
                {
                    return BadRequest("Start date must be before end date");
                }

                int totalCallsCountInPeriod = await _callDetailRecordService.GetTotalCallsCountInPeriodAsync(startDate, endDate);
                return Ok(new { totalCallsCountInPeriod, startDate, endDate });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CDRController)} error occurred at {nameof(GetTotalCallsCountInPeriod)} action.");
                return StatusCode(500, $"Error retrieving total calls count in period: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the total cost of all calls within a specified time period
        /// </summary>
        /// <param name="startDate">The beginning of the period in ISO 8601 format (YYYY-MM-DD)</param>
        /// <param name="endDate">The end of the period in ISO 8601 format (YYYY-MM-DD)</param>
        /// <remarks>
        /// Example: /api/CDR/total-cost-in-period?startDate=2023-01-01&endDate=2023-12-31
        /// </remarks>
        /// <returns>Total cost of calls that occurred during the specified period</returns>
        [HttpGet("total-cost-in-period")]
        public async Task<IActionResult> GetTotalCostInPeriod([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            try
            {
                if (startDate >= endDate)
                {
                    return BadRequest("Start date must be before end date");
                }

                decimal totalCostInPeriod = await _callDetailRecordService.GetTotalCostInPeriodAsync(startDate, endDate);
                return Ok(new { totalCostInPeriod, startDate, endDate });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CDRController)} error occurred at {nameof(GetTotalCostInPeriod)} action.");
                return StatusCode(500, $"Error retrieving total cost in period: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a list of the longest calls in the system
        /// </summary>
        /// <param name="count">Number of longest calls to retrieve</param>
        /// <returns>Collection of call records sorted by duration in descending order</returns>
        [HttpGet("longest-calls")]
        public async Task<IActionResult> GetLongestCalls([FromQuery] int count = 10)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than zero");
                }

                IEnumerable<CallDetailRecordDto> longestCalls = await _callDetailRecordService.GetLongestCallsAsync(count);
                return Ok(longestCalls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CDRController)} error occurred at {nameof(GetLongestCalls)} action.");
                return StatusCode(500, $"Error retrieving longest calls: {ex.Message}");
            }
        }

    }
}
