using System;
using System.Web;
using Aqueduct.DataAccess;
using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.EventHandlers;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.StructureMap;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Classes;
using Aqueduct.Web;
using Aqueduct.Web.Configuration;
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