using CDRPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDRPlatform.Domain.Interfaces.Services
{
    public interface ICallDetailRecordService
    {
        Task AddCallDetailRecordsAsync(IEnumerable<CallDetailRecord> records);
    }
}
