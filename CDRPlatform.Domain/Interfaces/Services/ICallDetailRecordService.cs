using CDRPlatform.Domain.Models;

namespace CDRPlatform.Domain.Interfaces.Services
{
    public interface ICallDetailRecordService
    {
        Task AddCallDetailRecordsAsync(Stream records);
    }
}
