using System.IO;
using System.Web;
using Aqueduct.Common.Context;
using Aqueduct.Common.Context.ModeLoaders;
using Aqueduct.Extensions;

namespace Aqueduct.Web
{
    public class WebContext : BaseContext
    {
        /// <summary>
        /// Initializes a new instance of the WebContext class
        /// <param name="hostName">current Website's host</param>
        /// </summary>
        public WebContext(string hostName) 
            : base(new FileApplicationModeLoader(), hostName) 
        {
            
        }
        /// <summary>
        /// Initializes a new instance of the WebContext class.
        /// </summary>
        public WebContext(IApplicationModeLoader modeLoader, string hostName)
            : base(modeLoader, hostName)
        {
            
        }

        /// <summary>
        /// Resolves virtualPaths relative to a website's folder
        /// </summary>
        /// <param name="virtualPath">Path relative to the website folder</param>
        /// <returns>Full filesystem path</returns>
        public override string ResolvePath(string virtualPath)
        {
			string processedPath = virtualPath.IsNullOrEmpty() ? virtualPath : virtualPath.TrimStart('~', '/');
			return Path.Combine(HttpRuntime.AppDomainAppPath, processedPath);
        }
    }
}
