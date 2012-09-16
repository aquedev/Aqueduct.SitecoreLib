using System;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.StructureMap;
using StructureMap;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Web
{
    public class Global : System.Web.HttpApplication
    {
        
        protected void Application_Start(object sender, EventArgs e)
        {
            
            ObjectFactory.Initialize(x =>
                                         {
                                             x.AddRegistry(new SimpleRegistry("Aqueduct.SitecoreLib.Examples.SampleApplication"));
                                         });

            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Context.Server.GetLastError() != null)
                return;
        }
    }
}