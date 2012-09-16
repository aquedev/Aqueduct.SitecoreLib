using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets.Social;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Maps.Widgets
{
    public class FacebookWidgetMap : Map<FacebookWidget>
    {
        public override string TemplatePath
        {
            get { return "Aqueduct/Widgets/Social/FacebookWidget"; }
        }
    }
}