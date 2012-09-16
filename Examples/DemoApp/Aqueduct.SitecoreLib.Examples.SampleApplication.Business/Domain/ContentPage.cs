using System.Collections.Generic;
using Aqueduct.SitecoreLib.Domain;
using Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain.Widgets;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Business.Domain
{
    public class ContentPage : DomainEntity
    {
        public IList<IWidget> Widgets { get; set; }
        public string Headline { get; set; }
        public Image Image { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
    }
}