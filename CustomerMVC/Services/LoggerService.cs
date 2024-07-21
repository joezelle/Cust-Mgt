namespace CustomerMgt.MVC.Services
{
    
        public class LoggerService : ILoggerService
        {
            private readonly ILogger<LoggerService> _logger;

           
            public LoggerService(ILogger<LoggerService> logger)
            {
                _logger = logger;
            }

        public void LogError(Exception ex, string message)
        {
            _logger.LogError(ex, "An error occurred: {Message}. Source: {Source}. StackTrace: {StackTrace}. InnerException: {InnerException}",
                             ex.Message, ex.Source, ex.StackTrace, ex.InnerException?.Message ?? "None");
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug("Debug: {Message}", message);
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation("Information: {Message}", message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning("Warning: {Message}", message);
        }
        }
    
}
