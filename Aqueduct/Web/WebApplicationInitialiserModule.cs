using System.Web;

namespace Aqueduct.Web
{
    public class WebApplicationInitialiserModule : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += WebApplicationInitialiser.HandleBeginRequest;
        }

    }
}
