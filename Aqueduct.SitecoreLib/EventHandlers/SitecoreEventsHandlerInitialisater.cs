using System.Web;
using Aqueduct.Web;
using Sitecore.Events;

namespace Aqueduct.SitecoreLib.EventHandlers
{
    /// <summary>
    /// Methods of the class are hooked up with the sitecore events through the web.config
    /// </summary>
    public class SitecoreEventsHandlerInitialisater : InitialisationAction
    {
        public SitecoreEventsHandlerInitialisater() : base(InitialisationStage.OnStart)
        {
            InitAction = Initialise;
        }

        private void Initialise(HttpApplication obj)
        {
            Event.Subscribe("publish:end", (sender, e) => SitecoreEvents.InvokeOnPublishEnd(sender, e));
            Event.Subscribe("publish:itemProcessed", (sender, e) => SitecoreEvents.InvokeOnItemProcessed(sender, e));
        }
    }
}