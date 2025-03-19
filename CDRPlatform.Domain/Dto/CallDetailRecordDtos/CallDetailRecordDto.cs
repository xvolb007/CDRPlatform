using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDRPlatform.Domain.Dto.CallDetailRecordDtos
{
    public class CallDetailRecordDto
    {
        public long? CallerId { get; set; }
        public long? Recipient { get; set; }
        public DateOnly CallDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Duration { get; set; }
        public decimal Cost { get; set; }
        public string? Reference { get; set; }
        public string? Currency { get; set; }
    }
}
