using System;
using System.Web.UI;
using Aqueduct.Presentation;
using StructureMap;

namespace Aqueduct.Web
{
    public class MVPUserControl<TPresenter> : UserControl
       where TPresenter : AbstractPresenter
    {
        public bool IsFirstViewing
        {
            get { return !IsPostBack; }
        }

        private readonly TPresenter m_presenter;
        public MVPUserControl()
        {
            m_presenter = ObjectFactory.GetInstance<TPresenter>();
        }

        protected override void OnInit(EventArgs e)
        {
            m_presenter.View = this;

            IntegrateWithPresenter(m_presenter);
            m_presenter.Init();

            base.OnInit(e);
        }

      
        public virtual void IntegrateWithPresenter(TPresenter presenter)
        {
        }
    }
}
