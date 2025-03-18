using CDRPlatform.Domain.Models;

namespace CDRPlatform.Domain.Interfaces.Repositories
{
    public interface ICallDetailRecordRepository
    {
        Task AddCallDetailRecordsAsync(IEnumerable<CallDetailRecord> records);
    }
}
