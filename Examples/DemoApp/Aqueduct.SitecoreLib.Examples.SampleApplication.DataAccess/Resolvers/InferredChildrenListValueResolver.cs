using System.Collections;
using Aqueduct.Domain;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Resolvers
{
    public class InferredChildrenListResolver<TChild> : ChildrenListResolver<TChild> where TChild : class, ISitecoreDomainEntity
    {
        protected override TChild AddItem(Item child, Aqueduct.SitecoreLib.DataAccess.IMap map, ReadOnlyRepository repository, IList list)
        {
            var entity = repository.Get(child.ID.Guid);
            if (entity is TChild)
            {
                list.Add(entity);
                return entity as TChild;
            }
            return default(TChild);
        }
    }
}
