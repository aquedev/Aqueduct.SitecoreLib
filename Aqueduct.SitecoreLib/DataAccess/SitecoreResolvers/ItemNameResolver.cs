using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers
{
	public class ItemNameResolver : ISitecoreResolver
	{
		public object Resolve(Item item)
		{
			return item.Name;
		}
	}
}
