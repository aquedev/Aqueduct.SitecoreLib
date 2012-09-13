using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Aqueduct.SitecoreLib.Search.DynamicFields
{
    public class FullPathField : BaseDynamicField
    {
        public override string ResolveValue(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            return item.Paths.FullPath;
        }
    }
}
