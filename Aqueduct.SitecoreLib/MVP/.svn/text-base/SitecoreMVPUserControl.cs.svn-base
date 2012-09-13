using System;
using System.Web.UI;
using Aqueduct.Presentation;
using StructureMap;

namespace Aqueduct.SitecoreLib.MVP
{
    public class SitecoreMVPUserControl<TPresenter> : UserControl
         where TPresenter : AbstractPresenter
    {
        public bool IsFirstViewing
        {
            get { return !IsPostBack; }
        }

        private readonly TPresenter m_presenter;
        public SitecoreMVPUserControl()
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

       public Guid ItemId
        {
            get { return Sitecore.Context.Item.ID.Guid; }
        }

        public string CurrentItemName
        {
            get { return Sitecore.Context.Item.Name; }
        }
    }
}
