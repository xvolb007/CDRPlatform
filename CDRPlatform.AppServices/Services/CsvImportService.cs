using CDRPlatform.AppServices.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDRPlatform.AppServices.Services
{
    public class CsvImportService : ICsvImportService
    {
        private readonly CsvConfiguration _defaultConfiguration;
        public CsvImportService()
        {
            _defaultConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };
        }
        public async Task<List<T>> ReadCSV<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, _defaultConfiguration))
            {
                csv.Context.TypeConverterOptionsCache.GetOptions<DateOnly>().Formats = new[] { "dd/MM/yyyy" };
                return await Task.Run(() => csv.GetRecords<T>().ToList());
            }
        }
    }
}
