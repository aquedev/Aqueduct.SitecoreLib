using System;
using System.IO;
using System.Text;
using Aqueduct.Configuration;
using Aqueduct.Extensions;
using log4net.Config;

namespace Aqueduct.Diagnostics
{
    internal static class Log4NetInitialiser
    {
        internal static void Initialise(string configFilePath)
        {
            if (string.IsNullOrEmpty(configFilePath))
                throw new ArgumentException("Configuration file path not specified.", "configFilePath");

            SetInternalDebugging();
            XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));
        }

        internal static void InitialiseUsingConfig()
        {
            SetInternalDebugging();
            string xmlConfig = Config.Get("Diagnostics.Config", "");
            if (xmlConfig.IsNotEmpty())
            {
                using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlConfig)))
                {
                    XmlConfigurator.Configure(stream);
                }
            }
        }

        private static void SetInternalDebugging()
        {
            log4net.Util.LogLog.InternalDebugging = Config.Get<bool>("Diagnostics.InternalDebugMode", false);
        }
    }
}
