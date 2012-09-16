using System;
using System.Web.UI;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Presentation;
using StructureMap;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Classes
{
    public class MVPUserControl<TPresenter> : UserControl, IMVPView
        where TPresenter : MVPPresenter
    {
        public bool IsFirstViewing
        {
            get { return !IsPostBack; }
        }

        public Guid CurrentItemId
        {
            get { return Sitecore.Context.Item.ID.Guid; }
        }

        public string CurrentItemName
        {
            get { return Sitecore.Context.Item.Name; }
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

        protected override void OnLoad(EventArgs e)
        {
            m_presenter.Load();

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            m_presenter.PreRender();

            base.OnPreRender(e);
        }

        public virtual void IntegrateWithPresenter(TPresenter presenter)
        {
        }
    }
}