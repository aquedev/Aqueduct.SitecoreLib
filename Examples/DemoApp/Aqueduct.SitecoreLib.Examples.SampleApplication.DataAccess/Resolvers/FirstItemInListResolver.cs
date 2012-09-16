using System.Linq;
using Aqueduct.Domain;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using StructureMap;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Resolvers
{
    public class FirstItemInListResolver<T> : ISitecoreResolver where T : ISitecoreDomainEntity
    {
        private readonly string m_fieldName;

        public FirstItemInListResolver(string fieldName)
        {
            m_fieldName = fieldName;
        }

        public object Resolve(Item item)
        {
            return ResolveItem(item);
        }

        protected virtual T ResolveItem(Item item)
        {
            var field = (MultilistField)item.Fields[m_fieldName];
            var repository = ObjectFactory.GetInstance<IReadOnlyRepository>();

            if (field != null && field.TargetIDs.Any())
                return repository.Get<T>(field.TargetIDs.First().Guid);

            return default(T);
        }
    }
}
