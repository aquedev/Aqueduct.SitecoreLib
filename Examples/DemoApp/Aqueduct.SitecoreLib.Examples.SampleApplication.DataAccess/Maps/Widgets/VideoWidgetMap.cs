using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Maps.Widgets
{
    public class VideoWidgetMap : Map<VideoWidget>
    {
        public VideoWidgetMap()
        {
            CopyMappingsFrom(new ImageWidgetMap());
        }

        public override string TemplatePath
        {
            get { return "Aqueduct/Widgets/VideoWidget"; }
        }
    }
}
