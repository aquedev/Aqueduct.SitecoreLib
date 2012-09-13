using Aqueduct.Diagnostics;

namespace Aqueduct.SitecoreLib.DataAccess
{
    internal class DataAccessLogger
    {
        private const string LoggerName = "Aqueduct.SitecoreLib.DataAccess";

        private static ILogger m_logger;
        
        internal static ILogger Logger
        {
            get { return m_logger ?? (m_logger = AppLogger.GetNamedLogger (LoggerName)); }
            set { m_logger = value; }
        }
     }
}
