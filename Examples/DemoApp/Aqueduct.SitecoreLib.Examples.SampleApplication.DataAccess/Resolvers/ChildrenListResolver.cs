using System;
using System.Collections;
using System.Collections.Generic;
using Aqueduct.Common;
using Aqueduct.Domain;
using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Resolvers
{
    public class ChildrenListResolver<TChild> : ISitecoreResolver
       where TChild : ISitecoreDomainEntity
    {
        public object Resolve(Item item)
        {
            var genericParameterType = GetType().GetGenericArguments()[0];

            return CreateLazyList(genericParameterType, () =>
            {
                var repository = new ReadOnlyRepository();
                var map = MapFinder.FindMap(genericParameterType);
                var list = CreateEmptyTypedList(genericParameterType);
                ProcessChildren(item, map, repository, list);
                return list;
            });
        }

        protected virtual void ProcessChildren(Item item, IMap map, ReadOnlyRepository repository, IList list)
        {
            foreach (Item child in item.Children)
            {
                AddItem(child, map, repository, list);
            }
        }

        protected virtual TChild AddItem(Item child, IMap map, ReadOnlyRepository repository, IList list)
        {
            if (child.DescendsFromTemplate(map.TemplatePath))
            {
                var entity = repository.Get<TChild>(child);
                list.Add(entity);
                return entity;
            }
            return default(TChild);
        }

        private static IList CreateLazyList(Type genericParameterType, Func<IEnumerable> loader)
        {
            return Activator.CreateInstance(typeof(LazyList<>).MakeGenericType(genericParameterType), loader) as IList;
        }

        private static IList CreateEmptyTypedList(Type argType)
        {
            return Activator.CreateInstance(typeof(List<>).MakeGenericType(argType)) as IList;
        }
    }
}
