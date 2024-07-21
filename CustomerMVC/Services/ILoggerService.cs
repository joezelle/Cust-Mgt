namespace CustomerMgt.MVC.Services
{
    
        public interface ILoggerService
        {
            void LogError(Exception ex, string message);
            void LogWarning(string message);
            void LogDebug(string message);
            void LogInfo(string message);
        }
    
}
