using System;
using System.IO;
using System.Reflection;
using Aqueduct.Extensions;

namespace Aqueduct.Common.Context
{
    public abstract class BaseContext : IContext
    {
        protected ApplicationMode _appMode;
        protected string _serverName = "";

        public BaseContext(IApplicationModeLoader modeLoader, string serverName)
        {
            if (modeLoader == null)
                throw new ArgumentNullException("modeLoader", "modeLoader is null.");
            
            _appMode = modeLoader.Load(this);
            _serverName = serverName;
        }

        public BaseContext(ApplicationMode appMode, string serverName)
        {
            _appMode = appMode;
            _serverName = serverName;
        }

        #region IContext Members

        public string ServerName
        {
            get { return _serverName; }
        }

        public virtual ApplicationMode AppMode
        {
            get { return _appMode; }
            protected set { _appMode = value; }
        }

        public virtual string ResolvePath (string virtualPath)
        {
            if (File.Exists (virtualPath) || Path.IsPathRooted (virtualPath))
                return virtualPath;

            string processedPath = virtualPath.IsNullOrEmpty () ? virtualPath : virtualPath.TrimStart ('~', '/');
            string assemblyFolder = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location);
            return Path.Combine (assemblyFolder, processedPath);
        }
        #endregion
    }
}