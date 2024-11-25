using Book_Commerce.Models;

namespace Book_Commerce.LogService
{
    public class LogService : ILogService
    {
        private readonly string _logFilePath;

        public LogService(IWebHostEnvironment env) // IWebHostEnvironment MVC nin içini temsil eder.
        {
            _logFilePath = Path.Combine(env.ContentRootPath, "Logging", "Logs.txt");
        }
        public async Task<ErrorLog> LogErrorAsync(Exception ex, int statusCode)
        {
            var errrorLog = new ErrorLog()
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                CreatedAt = DateTime.Now,
                StatusCode = statusCode
            };
            var logContent = $"[{errrorLog.CreatedAt}\nError: {errrorLog.Message}\nTackTrace: {errrorLog.StackTrace}\n\n]";

            await File.AppendAllTextAsync(_logFilePath, logContent);
            return errrorLog;
        }
    }
}
