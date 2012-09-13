using System;
using System.Collections.Generic;
using Aqueduct.Domain;
using Aqueduct.Extensions;
using Aqueduct.SitecoreLib.Domain;
using Sitecore.Data.Items;
using Sitecore.Data.Templates;

namespace Aqueduct.SitecoreLib.DataAccess.Repositories
{
    public interface IReadOnlyRepository
    {
        TDomainEntity Get<TDomainEntity>(Guid id) where TDomainEntity : ISitecoreDomainEntity;
        TDomainEntity Get<TDomainEntity>(Item item) where TDomainEntity : ISitecoreDomainEntity;
        IList<TDomainEntity> Get<TDomainEntity>(IEnumerable<Guid> ids) where TDomainEntity : ISitecoreDomainEntity;
        DomainEntity Get(Guid id);
        Item GetBaseItem(Guid id);
    }

    public class ReadOnlyRepository : Repository<ISitecoreDomainEntity>, IReadOnlyRepository
    {
        public TDomainEntity Get<TDomainEntity>(Guid id)
            where TDomainEntity : ISitecoreDomainEntity
        {
            return GetEntity<TDomainEntity>(id);
        }

        public ReadOnlyRepository()
        {
        }

        public ReadOnlyRepository(string databaseName)
            : base(databaseName)
        {
        }

        public DomainEntity Get(Guid id)
        {
            if (id.IsEmpty())
                return null;

            Item item = GetItem(id);
            return ParseItemUsingInferredTemplatePath<DomainEntity>(item);
        }

        public Item GetBaseItem(Guid id)
        {
            return GetItem(id);
        }

        public IList<TDomainEntity> Get<TDomainEntity>(IEnumerable<Guid> ids)
            where TDomainEntity : ISitecoreDomainEntity
        {
            IMap map = MapFinder.FindMap<TDomainEntity>();
            Template template = GetTemplate(map);

            var results = new List<TDomainEntity>();
            foreach (Guid id in ids)
            {
                Item item = GetItem(id);
                if (item != null && item.DescendsFromTemplate(template))
                {
                    results.Add((TDomainEntity)ParseItem(item, map));
                }
            }

            return results;
        }

        public IList<TDomainEntity> GetItems<TDomainEntity>(string xpathQuery)
            where TDomainEntity : ISitecoreDomainEntity
        {
            var results = new List<TDomainEntity>();
            Item[] list = CurrentDatabase.SelectItems(xpathQuery);
            foreach (Item item in list)
            {
                results.Add(ParseItem<TDomainEntity>(item));
            }
            return results;
        }

        public TDomainEntity Get<TDomainEntity>(Item item)
            where TDomainEntity : ISitecoreDomainEntity
        {
            return ParseItem<TDomainEntity>(item);
        }

        public TDomainEntity Get<TDomainEntity>(string path)
            where TDomainEntity : ISitecoreDomainEntity
        {
            var item = GetItem(path);
            return ParseItem<TDomainEntity>(item);
        }
    }
}
