using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;


namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Presentation.Controls.Widgets
{
    public class WidgetAreaPresenter : MVPPresenter<IWidgetArea>
    {
        private readonly IReadOnlyRepository m_repository;

        public WidgetAreaPresenter(IReadOnlyRepository repository)
        {
            m_repository = repository;
        }

        public override void Init()
        {
            var page = m_repository.Get<BaseWidgetPage>(View.CurrentItemId);

            View.Widgets = page.Widgets;
        }
    }
}