using System;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Presentation
{
    public interface IMVPView
    {
        bool IsFirstViewing { get; }
        Guid CurrentItemId { get; }
        string CurrentItemName { get; }
        bool Visible { get; set; }
    }
}