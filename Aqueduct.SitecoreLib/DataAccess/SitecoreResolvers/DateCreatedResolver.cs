using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers
{
	public class DateCreatedResolver : ISitecoreResolver
	{
		public object Resolve(Item item)
		{
			return item.Statistics.Created;
		}
	}
}
