using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers
{
    public class DateModifiedResolver : ISitecoreResolver
    {
        public object Resolve(Item item)
        {
            return item.Statistics.Updated;
        }
    }
}
