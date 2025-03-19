using CDRPlatform.Domain.Dto.CallDetailRecordDtos;
using CDRPlatform.Domain.Models;

namespace CDRPlatform.Domain.Interfaces.Services
{
    public interface ICallDetailRecordService
    {
        Task AddCallDetailRecordsAsync(Stream records);
        Task<IEnumerable<CallDetailRecordDto>> GetCallsByCallerIdAsync(long callerId);
        Task<IEnumerable<CallDetailRecordDto>> GetCallsByPageAsync(int page, int pageSize);
        Task<decimal> GetAverageCallCostAsync();
        Task<TimeSpan> GetLongestCallDurationAsync();
        Task<int> GetTotalCallsCountAsync();
        Task<int> GetTotalCallsCountInPeriodAsync(DateOnly startDate, DateOnly endDate);
        Task<decimal> GetTotalCostInPeriodAsync(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<CallDetailRecordDto>> GetLongestCallsAsync(int count);

    }
}
