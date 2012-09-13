using System;
using Aqueduct.Configuration;
using log4net;
using AppfailReporting;
namespace Aqueduct.Diagnostics
{
	/// <summary>
	/// Logs errors and messages using specified configuration. Cannot be inherited.
	/// </summary>
	public sealed class AppLogger
	{
        private static ILogger m_globalLogger = NullLogger.Instance;
        
        public static ILogger GlobalLogger
        {
            get { return m_globalLogger; }
        }

		/// <summary>
		/// Initialises <see cref="AppLogger"/> using the specified config file.
		/// </summary>
		/// <param name="configFilePath">Name of the config file to load configuration from.</param>
		/// <param name="loggerName">Name of the logger to use for logging.</param>
		/// <exception cref="ArgumentException">when <paramref name="configFilePath"/> is null or empty.</exception>
		/// /// <exception cref="ArgumentException">when <paramref name="loggerName"/> is null or empty.</exception>
		public static void Initialise(string configFilePath, string loggerName)
		{
            Log4NetInitialiser.Initialise(configFilePath);

			if (string.IsNullOrEmpty(loggerName))
				throw new ArgumentException("Logger name not specified.", "loggerName");

            m_globalLogger = new Logger(loggerName);
		}

        /// <summary>
        /// Initialises <see cref="AppLogger"/> using the Aqueduct.Config file.
        /// </summary>
        /// <remarks>Uses the contents of the Diagnostics.Config setting to Initialise the logger</remarks>
        /// <remarks>To enable the intrelnal log debugging set Diagnostics.InternalDebugMode setting to true</remarks>
        public static void InitialiseFromConfig(string loggerName)
        {
            Log4NetInitialiser.InitialiseUsingConfig();
            Config.SettingsChanged += (sender, args) => { Log4NetInitialiser.InitialiseUsingConfig(); };

            if (string.IsNullOrEmpty(loggerName))
                throw new ArgumentException("Logger name not specified.", "loggerName");

            m_globalLogger = new Logger(loggerName);
        }

		public static void AddGlobalCustomParameter(string name, string value )
		{
			GlobalContext.Properties[name] = value;
		}

        /// <summary>
        /// Returns a new instance of ILogger with a given name 
        /// </summary>
        /// <param name="logName">Name of the logger to use for logging.</param>
        /// <remarks>Need to have called Initialise before you can get a working logger</remarks>
        public static ILogger GetNamedLogger(string logName)
        {
            return new Logger(logName);
        }

        /// <summary>
        /// Returns a new instance of ILogger with a name taken from a type
        /// </summary>
        /// <param name="loggedType">The type which will be used as the logger's name.</param>
        /// <remarks>Need to have called Initialise before you can get a working logger</remarks>
        public static ILogger GetNamedLogger(Type loggedType)
        {
            return new Logger(loggedType);
        }

		/// <summary>
		/// Writes DEBUG-level message to the configured log.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public static void LogDebugMessage(string message)
		{
			m_globalLogger.LogDebugMessage(message);
		}

		/// <summary>
		/// Writes INFO-level message to the configured  log.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public static void LogInfoMessage(string message)
		{
            m_globalLogger.LogInfoMessage(message);
		}

		/// <summary>
		/// Writes WARNING-level message to the configured log.
		/// </summary>
		/// <param name="message">The message to log.</param>
		public static void LogWarningMessage(string message)
		{
            m_globalLogger.LogWarningMessage(message);
		}

		/// <summary>
		/// Writes ERROR-level message to the configured log.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="exception">The exception to log.</param>
		public static void LogError(string message, Exception exception)
		{
            m_globalLogger.LogError(message, exception);
            SendToAppFail(exception);
		}

		/// <summary>
		/// Writes FATAL-level message to the configured log.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="exception">The exception to log.</param>
		public static void LogFatalError(string message, Exception exception)
		{
            m_globalLogger.LogFatalError(message, exception);
		    SendToAppFail(exception);
		}


        private static void SendToAppFail(Exception exception)
        {
            try { exception.SendToAppfail(); }
            catch(Exception ex) { LogWarningMessage("Could not log to AppFAil"); } 
        }
	}
}
