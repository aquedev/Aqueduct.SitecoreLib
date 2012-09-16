using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets.Social;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Maps.Widgets
{
    public class GooglePlusWidgetMap : Map<GooglePlusWidget>
    {
        public override string TemplatePath
        {
            get { return "Aqueduct/Widgets/Social/GooglePlusWidget"; }
        }
    }
}