
namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Presentation
{
    public class MVPPresenter<TView> : MVPPresenter where TView : IMVPView
    {
        public new TView View
        {
            get { return (TView)base.View; }
            set { base.View = value; }
        }
    }

    public abstract class MVPPresenter
    {
        public object View { get; set; }

        public virtual void Init()
        {
        }

        public virtual void Load()
        {
        }

        public virtual void PreRender()
        {
        }

        public virtual void Render()
        {
        }
    }
}