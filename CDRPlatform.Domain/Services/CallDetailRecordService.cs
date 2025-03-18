using CDRPlatform.AppServices.Interfaces;
using CDRPlatform.Domain.Interfaces.Repositories;
using CDRPlatform.Domain.Interfaces.Services;
using CDRPlatform.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net.Security;

namespace CDRPlatform.Domain.Services
{
    public class CallDetailRecordService : ICallDetailRecordService
    {
        private readonly ILogger<CallDetailRecordService> _logger;
        private readonly ICallDetailRecordRepository _callDetailRecordRepository;
        private readonly ICsvImportService _csvImportService;
        public CallDetailRecordService(ILogger<CallDetailRecordService> logger, ICallDetailRecordRepository callDetailRecordRepository, ICsvImportService csvImportService)
        {
            _logger = logger;
            _callDetailRecordRepository = callDetailRecordRepository;
            _csvImportService = csvImportService;
        }
        public async Task AddCallDetailRecordsAsync(Stream csvStream)
        {
            var records = await _csvImportService.ReadCSV<CallDetailRecord>(csvStream);
            if (records == null || !records.Any())
            {
                _logger.LogWarning("No valid records found in the CSV file");
                return;
            }

            await _callDetailRecordRepository.AddCallDetailRecordsAsync(records);

            _logger.LogInformation("Successfully imported call detail records");
        
        }
    }
}
