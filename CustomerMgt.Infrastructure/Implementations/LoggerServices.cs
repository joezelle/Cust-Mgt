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
        //private static readonly Logger _log = LogManager.GetLogger("APPLog");

        private static ILogger _log = LogManager.GetCurrentClassLogger();
        public void Error(Exception ex)
        
        {
            _log.Error(ex, string.Format("Message : {0}  Source :{1}  StackTrace:{2} InnerException :{3}", ex.Message, ex.Source, ex.StackTrace, ex.InnerException));
            //StackifyLib.Logger.QueueException("Error", ex, (ex.Message + ex.Source + ex.StackTrace));
        }

        public void LogDebug(string message)
        {
            _log.Debug(message);
        }

        public void LogInfo(string message)
        {
            _log.Info(message);
        }

        public void Warning(string message)
        {
            _log.Warn(message);
        }
    }
}
