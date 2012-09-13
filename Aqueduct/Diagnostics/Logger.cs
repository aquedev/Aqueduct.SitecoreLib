using System;
using System.Collections.Generic;
using log4net;
using log4net.Core;

namespace Aqueduct.Diagnostics
{
    public class Logger : ILogger
    {
        private readonly static Type declaringType = typeof(Logger);

        private readonly ILog m_log;

        internal Logger(string logName)
        {
            m_log = LogManager.GetLogger(logName);
        }

        internal Logger(Type loggedType)
        {
            m_log = LogManager.GetLogger(loggedType);
        }

        public void LogDebugMessage(string message)
        {
            if (m_log.IsDebugEnabled)
                m_log.Debug(message);
        }

        public void LogInfoMessage(string message)
        {
            if (m_log.IsInfoEnabled)
                m_log.Info(message);
        }

        public void LogInfoMessage(string message, Dictionary<string, object> extraInfo)
        {
            LogEvent(Level.Info, message, extraInfo, null);
        }

        private void LogEvent(Level level, string message, Dictionary<string, object> extraInfo, Exception exception)
        {
            if (m_log.Logger.IsEnabledFor(level))
            {
                LoggingEvent loggingEvent = new LoggingEvent(declaringType, m_log.Logger.Repository, m_log.Logger.Name,
                                                             Level.Info, message, exception);
                if (extraInfo != null)
                {
                    foreach (KeyValuePair<string, object> pair in extraInfo)
                    {
                        loggingEvent.Properties[pair.Key] = pair.Value;
                    }
                }
                m_log.Logger.Log(loggingEvent);
            }
        }

        public void LogWarningMessage(string message)
        {
            if (m_log.IsWarnEnabled)
                m_log.Warn(message);
        }
        public void LogError(string message, Exception exception)
        {
            if (m_log.IsErrorEnabled)
                m_log.Error(message, exception);
        }

        public void LogError(string message, Exception exception, Dictionary<string, object> extraInfo)
        {
            LogEvent(Level.Error, message, extraInfo, exception);
        }

        public void LogFatalError(string message, Exception exception)
        {
            if (m_log.IsFatalEnabled)
                m_log.Fatal(message, exception);
        }
    }
}
