using System;
using System.Collections.Generic;

namespace Aqueduct.Diagnostics
{
    public interface ILogger
    {
        void LogDebugMessage(string message);
        void LogInfoMessage(string message);
        void LogInfoMessage(string message, Dictionary<string, object> extraInfo);
        void LogWarningMessage(string message);
        void LogError(string message, Exception exception);
        void LogError(string message, Exception exception, Dictionary<string, object> extraInfo);
        void LogFatalError(string message, Exception exception);
    }
}
