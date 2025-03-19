using AutoMapper;
using CDRPlatform.AppServices.Interfaces;
using CDRPlatform.Domain.Dto.CallDetailRecordDtos;
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
        private readonly IMapper _mapper;
        public CallDetailRecordService(ILogger<CallDetailRecordService> logger, ICallDetailRecordRepository callDetailRecordRepository, ICsvImportService csvImportService, IMapper mapper)
        {
            _logger = logger;
            _callDetailRecordRepository = callDetailRecordRepository;
            _csvImportService = csvImportService;
            _mapper = mapper;
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
        public async Task<IEnumerable<CallDetailRecordDto>> GetCallsByCallerIdAsync(long callerId)
        {
            var callRecords = await _callDetailRecordRepository.GetCallsByCallerIdAsync(callerId);
            return _mapper.Map<IEnumerable<CallDetailRecordDto>>(callRecords);
        }
        public async Task<IEnumerable<CallDetailRecordDto>> GetCallsByPageAsync(int page, int pageSize)
        {
            var callRecords = await _callDetailRecordRepository.GetCallsByPageAsync(page, pageSize);
            return _mapper.Map<IEnumerable<CallDetailRecordDto>>(callRecords);
        }
        public async Task<decimal> GetAverageCallCostAsync()
        {
            return await _callDetailRecordRepository.GetAverageCallCostAsync();
        }

        public async Task<TimeSpan> GetLongestCallDurationAsync()
        {
            return await _callDetailRecordRepository.GetLongestCallDurationAsync();
        }

        public async Task<int> GetTotalCallsCountAsync()
        {
            return await _callDetailRecordRepository.GetTotalCallsCountAsync();
        }

        public async Task<int> GetTotalCallsCountInPeriodAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _callDetailRecordRepository.GetTotalCallsCountInPeriodAsync(startDate, endDate);
        }

        public async Task<decimal> GetTotalCostInPeriodAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _callDetailRecordRepository.GetTotalCostInPeriodAsync(startDate, endDate);
        }

        public async Task<IEnumerable<CallDetailRecordDto>> GetLongestCallsAsync(int count)
        {
            var callRecords = await _callDetailRecordRepository.GetLongestCallsAsync(count);
            return _mapper.Map<IEnumerable<CallDetailRecordDto>>(callRecords);
        }

    }
}
