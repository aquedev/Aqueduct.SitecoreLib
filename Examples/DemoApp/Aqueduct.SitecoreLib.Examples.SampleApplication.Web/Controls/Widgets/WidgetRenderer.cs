using System;
using System.Web.UI;
using Aqueduct.Presentation;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Classes;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.Widgets
{
    public class WidgetRenderer : UserControl
    {
        public IWidget Widget { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            if (Widget == null) return;

            var controlPath = string.Format("~/Controls/Widgets/{0}Control.ascx", Widget.GetType().Name);

            var control = (IWidgetControl)LoadControl(controlPath);
            control.Widget = Widget;
            Controls.Add((Control)control);
        }
    }

    public interface IWidgetControl
    {
        IWidget Widget { set; }
    }
}