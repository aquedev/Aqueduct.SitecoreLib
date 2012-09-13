using System;
using System.Collections.Generic;
using Sitecore.Data.Items;
using Aqueduct.Domain;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public class DomainEntityRepository : Repository<ISitecoreDomainEntity>, IDomainEntityRepository
    {
        public override ISitecoreDomainEntity GetEntity(Guid id)
        {
            Item item = GetItem(id);
            return ParseItemUsingInferredTemplatePath<ISitecoreDomainEntity>(item);
        }

        public IList<ISitecoreDomainEntity> GetEntities(IEnumerable<Guid> ids, IMap map)
        {
            var entities = new List<ISitecoreDomainEntity>();
            foreach (Guid id in ids)
            {
                Item item = GetItem(id);

                if (item != null && item.DescendsFromTemplate(GetTemplate(map)))
                {
                    entities.Add(ParseItem(item, map));
                }
            }
            return entities;
        }
    }
}