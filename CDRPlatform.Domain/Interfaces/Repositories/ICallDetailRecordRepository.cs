using CDRPlatform.Domain.Models;

namespace CDRPlatform.Domain.Interfaces.Repositories
{
    public interface ICallDetailRecordRepository
    {
        Task AddCallDetailRecordsAsync(IEnumerable<CallDetailRecord> records);
        Task<IEnumerable<CallDetailRecord>> GetCallsByCallerIdAsync(long callerId);
        Task<IEnumerable<CallDetailRecord>> GetCallsByPageAsync(int page, int pageSize);
        Task<decimal> GetAverageCallCostAsync();
        Task<TimeSpan> GetLongestCallDurationAsync();
        Task<int> GetTotalCallsCountAsync();
        Task<int> GetTotalCallsCountInPeriodAsync(DateOnly startDate, DateOnly endDate);
        Task<decimal> GetTotalCostInPeriodAsync(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<CallDetailRecord>> GetLongestCallsAsync(int count);
    }
}
