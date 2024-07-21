using CustomerMgt.Core.Services;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogger = NLog.ILogger;

namespace CustomerMgt.Infrastructure.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        // Constructor Injection for ILogger
        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message} Source: {Source} StackTrace: {StackTrace} InnerException: {InnerException}",
                             ex.Message, ex.Source, ex.StackTrace, ex.InnerException?.Message);
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }

}
