using System;

namespace Aqueduct.Presentation
{
    public interface ISitecoreView : IAbstractView
    {
        Guid ItemId { get; }
    }
}
