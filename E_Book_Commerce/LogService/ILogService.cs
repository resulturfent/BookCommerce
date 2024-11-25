using Book_Commerce.Models;

namespace Book_Commerce.LogService
{
    public interface ILogService
    {
        Task<ErrorLog> LogErrorAsync(Exception ex, int statusCode);
    }
}
