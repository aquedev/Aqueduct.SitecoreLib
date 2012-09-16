using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Maps.Widgets
{
    public class ImageWidgetMap : Map<ImageWidget>
    {
        public ImageWidgetMap()
        {
            MapProperty(x => x.Title);
            MapProperty(x => x.Image);
            MapProperty(x => x.Strapline);
            MapProperty(x => x.Link);
        }

        public override string TemplatePath
        {
            get { return "Aqueduct/Widgets/ImageWidget"; }
        }
    }
}
