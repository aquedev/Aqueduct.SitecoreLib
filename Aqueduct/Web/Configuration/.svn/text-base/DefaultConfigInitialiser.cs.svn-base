using Aqueduct.Configuration;

namespace Aqueduct.Web.Configuration
{
    /// <summary>
    /// Used to initialise the AqueductConfig on the first request
    /// It calls Config.Initialize(new WebContext(app.Request.Url.Host))
    /// </summary>
    public class DefaultConfigInitialiser : InitialisationAction
    {
        public DefaultConfigInitialiser()
            : base(InitialisationStage.OnFirstRequest, app =>
                Config.Initialize(new WebContext(app.Request.Url.Host)))
        {

        }
    }
}
