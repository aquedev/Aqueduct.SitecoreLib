using System.Collections.Generic;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Presentation.Controls.Widgets
{
    public interface IWidgetArea : IMVPView
    {
        IList<IWidget> Widgets { set; }
    }
}
