using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Core.Services
{
    public interface ILoggerService
    {
        void Error(Exception ex);
        void Warning(string message);
        void LogDebug(string message);
        void LogInfo(string message);
    }
}
