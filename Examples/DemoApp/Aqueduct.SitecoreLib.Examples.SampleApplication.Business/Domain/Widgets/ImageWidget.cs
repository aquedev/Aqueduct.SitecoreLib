using Aqueduct.Domain;
using Aqueduct.SitecoreLib.Domain;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets
{
    public class ImageWidget : BaseWidget
    {
        public string Title { get; set; }
        public Image Image { get; set; }
        public string Strapline { get; set; }
        public Link Link { get; set; }
    }
}
