using System;
using System.Collections.Generic;

namespace Aqueduct.Diagnostics
{
    /// <summary>
    /// Null logger implementation. Doesn't do anything
    /// </summary>
    public class NullLogger : ILogger
    {
        private static ILogger _instance = new NullLogger();

        public static ILogger Instance
        {
            get { return _instance; }
        }

        private NullLogger() { }

        public void LogDebugMessage(string message)
        {

        }

        public void LogInfoMessage(string message)
        {

        }

        public void LogInfoMessage(string message, Dictionary<string, object> extraInfo)
        {
            
        }

        public void LogWarningMessage(string message)
        {

        }

        public void LogError(string message, Exception exception)
        {

        }

        public void LogError(string message, Exception exception, Dictionary<string, object> extraInfo)
        {

        }

        public void LogFatalError(string message, Exception exception)
        {

        }
    }
}
