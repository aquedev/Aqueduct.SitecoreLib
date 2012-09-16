using System.Collections.Generic;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Presentation.Controls.Content
{
    public interface IContentView : IMVPView
    {
        string Headline { set; }
        string Summary { set; }
        string Content { set; }
        IList<IWidget> Widgets { set; }
    }
}
