using System.Web.UI;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.Widgets
{
    public partial class ImageWidgetControl : UserControl, IWidgetControl
    {
        private ImageWidget m_imageWidget;
        protected ImageWidget ImageWidget
        {
            get { return m_imageWidget ?? new ImageWidget(); }
        }

        public IWidget Widget { set { m_imageWidget = (ImageWidget)value; } }

        protected string Strapline
        {
            get
            {
                return string.IsNullOrEmpty(ImageWidget.Strapline)
                           ? string.Empty
                           : string.Format("<p>{0}</p>", ImageWidget.Strapline);
            }
        }
    }
}