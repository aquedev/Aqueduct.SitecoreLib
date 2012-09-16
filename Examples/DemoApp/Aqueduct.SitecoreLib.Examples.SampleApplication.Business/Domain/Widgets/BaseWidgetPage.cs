using System.Collections.Generic;
using Aqueduct.SitecoreLib.Domain;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets
{
    public class BaseWidgetPage : DomainEntity
    {
        public IList<IWidget> Widgets { get; set; }
    }
}
