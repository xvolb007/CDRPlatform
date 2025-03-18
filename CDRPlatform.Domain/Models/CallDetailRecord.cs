using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace CDRPlatform.Domain.Models
{
    public class CallDetailRecord
    {
        public int Id { get; set; }
        [Name("caller_id")]
        public long? CallerId { get; set; }

        [Name("recipient")]
        public long? Recipient { get; set; }

        [Name("call_date")]
        public DateOnly CallDate { get; set; }

        [Name("end_time")]
        public TimeSpan EndTime { get; set; }

        [Name("duration")]
        public int Duration { get; set; }

        [Name("cost")]
        public decimal Cost { get; set; }

        [Name("reference")]
        public string? Reference { get; set; }

        [Name("currency")]
        public string? Currency { get; set; }
        
    }
}
