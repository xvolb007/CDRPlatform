

namespace CDRPlatform.AppServices.Interfaces
{
    public interface ICsvImportService
    {
        Task<List<T>> ReadCSV<T>(Stream file);
    }
}
