using CDRPlatform.Data.Contexts;
using CDRPlatform.Domain.Interfaces.Repositories;
using CDRPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CDRPlatform.Data.Repositories
{
    public class CallDetailRecordRepository : ICallDetailRecordRepository
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
        public async Task<IEnumerable<CallDetailRecord>> GetCallsByCallerIdAsync(long callerId)
        {
                return await _context.CallDetailRecord
                    .AsNoTracking()
                    .Where(cdr => cdr.CallerId == callerId)
                    .OrderByDescending(cdr => cdr.CallDate)
                    .ToListAsync();
        }
        public async Task<IEnumerable<CallDetailRecord>> GetCallsByPageAsync(int page, int pageSize)
        {
            return await _context.CallDetailRecord
                            .AsNoTracking()
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        }
        public async Task<decimal> GetAverageCallCostAsync()
        {
            return await _context.CallDetailRecord.AverageAsync(c => c.Cost);
        }

        public async Task<TimeSpan> GetLongestCallDurationAsync()
        {
            int maxDurationSeconds = await _context.CallDetailRecord.MaxAsync(record => record.Duration);
            return TimeSpan.FromSeconds(maxDurationSeconds);
        }

        public async Task<int> GetTotalCallsCountAsync()
        {
            return await _context.CallDetailRecord.CountAsync();
        }

        public async Task<int> GetTotalCallsCountInPeriodAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _context.CallDetailRecord
                .CountAsync(c => c.CallDate >= startDate && c.CallDate <= endDate);
        }

        public async Task<decimal> GetTotalCostInPeriodAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _context.CallDetailRecord
                .Where(c => c.CallDate >= startDate && c.CallDate <= endDate)
                .SumAsync(c => c.Cost);
        }

        public async Task<IEnumerable<CallDetailRecord>> GetLongestCallsAsync(int count)
        {
            return await _context.CallDetailRecord
                .OrderByDescending(c => c.Duration)
                .Take(count)
                .ToListAsync();
        }
    }
}
