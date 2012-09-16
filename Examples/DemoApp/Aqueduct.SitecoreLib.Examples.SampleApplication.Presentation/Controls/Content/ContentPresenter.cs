using Aqueduct.Presentation;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Presentation.Controls.Content
{
    public class ContentPresenter : MVPPresenter<IContentView>
    {
        private readonly IReadOnlyRepository m_repository;

        public ContentPresenter(IReadOnlyRepository repository)
        {
            m_repository = repository;
        }

        public override void Init()
        {
            var page = m_repository.Get<ContentPage>(View.CurrentItemId);
            if (page == null) return;

            View.Headline = page.Headline;
            View.Summary = page.Summary;
            View.Content = page.Content;

            if (page.Widgets != null)
                View.Widgets = page.Widgets;
        }
    }
}
