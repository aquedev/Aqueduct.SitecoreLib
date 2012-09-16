using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Presentation.Controls.Widgets;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Classes;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.Widgets;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls
{
    public partial class WidgetArea : MVPUserControl<WidgetAreaPresenter>, IWidgetArea
    {
        private IList<IWidget> m_widgets;
        public IList<IWidget> Widgets
        {
            set
            {
                m_widgets = value;
                rptWidgets.DataSource = value;
                rptWidgets.DataBind();
            }
        }

        public override bool Visible
        {
            get { return (m_widgets == null || m_widgets.Any()) && base.Visible; }
            set
            {
                base.Visible = value;
            }
        }

        protected void rptWidgets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var currentWidget = e.Item.DataItem;
            var renderer = (WidgetRenderer)e.Item.FindControl("Renderer");

            renderer.Widget = (IWidget)currentWidget;
        }
    }
}