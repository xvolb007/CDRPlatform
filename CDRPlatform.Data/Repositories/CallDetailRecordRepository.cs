using CDRPlatform.Data.Contexts;
using CDRPlatform.Domain.Interfaces.Repositories;
using CDRPlatform.Domain.Models;
using Microsoft.Extensions.Logging;

namespace CDRPlatform.Data.Repositories
{
    public class CallDetailRecordRepository: ICallDetailRecordRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CallDetailRecordRepository> _logger;
        public CallDetailRecordRepository(AppDbContext context, ILogger<CallDetailRecordRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddCallDetailRecordsAsync(IEnumerable<CallDetailRecord> records)
        {
            try
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await _context.CallDetailRecord.AddRangeAsync(records);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        _logger.LogInformation($"Successfully imported {records.Count()} records");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        _logger.LogError(ex, "Error during import. Transaction rolled back.");
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to import records");
                throw;
            }
        }
    }
}
