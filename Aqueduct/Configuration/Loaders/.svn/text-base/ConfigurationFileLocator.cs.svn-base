using System.IO;
using Aqueduct.Common.Context;

namespace Aqueduct.Configuration.Loaders
{
    public class ConfigurationFileLocator : IConfigurationFileLocator
    {
        private readonly IContext _context;
        private readonly string[] _locations;

        /// <summary>
        /// Initializes a new instance of the ConfigFileLocator class.
        /// </summary>
        /// <param name="context"></param>
        public ConfigurationFileLocator (IContext context)
        {
            _context = context;
            _locations = new[] {"~/AppSettings.config", "~/Config/AppSettings.config"};
        }

        #region IConfigurationFileLocator Members

        public string GetConfigurationFile ()
        {
            foreach (string location in _locations)
            {
                string fullPath = _context.ResolvePath (location);
                if (File.Exists (fullPath))
                    return fullPath;
            }

            throw new FileNotFoundException ("Cannot find configuration file. Tried location: " +
                                             string.Join (" | ", _locations));
        }

        #endregion
    }
}