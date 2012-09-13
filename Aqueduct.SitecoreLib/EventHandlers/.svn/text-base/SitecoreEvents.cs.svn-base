using System;

namespace Aqueduct.SitecoreLib.EventHandlers
{
    public static class SitecoreEvents
    {
        public static event EventHandler OnPublishEnd;
        public static event EventHandler OnItemProcessed;
        /// <summary>
        /// ONLY TO BE USED BY SitecoreEventsHandlerInitialisater class
        /// </summary>
        internal static void InvokeOnPublishEnd(object sender, EventArgs e)
        {
            EventHandler handler = OnPublishEnd;
            if (handler != null) handler(sender, e);
        }

        /// <summary>
        /// ONLY TO BE USED BY SitecoreEventsHandlerInitialisater class
        /// </summary>
        internal static void InvokeOnItemProcessed(object sender, EventArgs e)
        {
            EventHandler handler = OnItemProcessed;
            if (handler != null) handler(sender, e);
        }
    }
}
