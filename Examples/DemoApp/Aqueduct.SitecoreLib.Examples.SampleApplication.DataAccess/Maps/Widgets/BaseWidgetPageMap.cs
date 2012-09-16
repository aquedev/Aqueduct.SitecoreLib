using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;
using Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Resolvers;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Maps.Widgets
{
    public class BaseWidgetPageMap : Map<BaseWidgetPage>
    {
        public BaseWidgetPageMap()
        {
            MapProperty(x => x.Widgets).SetResolver(new InferredMultiListValueResolver<IWidget>("Widgets"));
        }

        public override string TemplatePath
        {
            get { return "Aqueduct/Fake/Template"; }
        }

        public override bool SkipTemplateChecking
        {
            get { return true; }
        }
    }
}
