using System.Collections;
using Aqueduct.Domain;
using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Resolvers
{
    public class ChildrenPlusSelfResolver<TChild> : ChildrenListResolver<TChild> where TChild : ISitecoreDomainEntity
    {
        protected override void ProcessChildren(Item item, IMap map, ReadOnlyRepository repository, IList list)
        {
            //Add self.
            AddItem(item, map, repository, list);

            base.ProcessChildren(item, map, repository, list);
        }
    }
}
