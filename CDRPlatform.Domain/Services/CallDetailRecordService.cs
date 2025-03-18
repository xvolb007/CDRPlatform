using CDRPlatform.Domain.Interfaces.Repositories;
using CDRPlatform.Domain.Interfaces.Services;
using CDRPlatform.Domain.Models;
using Microsoft.Extensions.Logging;

namespace CDRPlatform.Domain.Services
{
    public class CallDetailRecordService : ICallDetailRecordService
    {
        private readonly ILogger<CallDetailRecordService> _logger;
        private readonly ICallDetailRecordRepository _callDetailRecordRepository;
        public CallDetailRecordService(ILogger<CallDetailRecordService> logger, ICallDetailRecordRepository _callDetailRecordRepository)
        {
            _logger = logger;
            _callDetailRecordRepository = _callDetailRecordRepository;
        }
        public async Task AddCallDetailRecordsAsync(IEnumerable<CallDetailRecord> records)
        {

        }
    }
}
