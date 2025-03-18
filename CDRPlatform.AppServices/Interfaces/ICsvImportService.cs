using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDRPlatform.AppServices.Interfaces
{
    public interface ICsvImportService
    {
        Task<List<T>> ReadCSV<T>(Stream file);
    }
}
