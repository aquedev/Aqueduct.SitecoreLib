using System.Collections.Generic;
using System.Linq;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Presentation.Controls.Content;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Classes;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls
{
    public partial class ContentControl : MVPUserControl<ContentPresenter>, IContentView
    {
        public string Headline { set; protected get; }
        public string Summary { set; protected get; }
        public string Content { set; protected get; }

        public IList<IWidget> Widgets
        {
            set
            {
                if (value.Any())
                {
                   // RightWidgets.Widgets = value;
                    //RightWidgets.Visible = true;

                    Show3Column = true;
                }
            }
        }

        protected bool Show3Column { get; set; }
    }
}