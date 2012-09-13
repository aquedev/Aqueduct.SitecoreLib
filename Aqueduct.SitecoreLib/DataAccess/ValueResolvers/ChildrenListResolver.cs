using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aqueduct.Common;
using Aqueduct.Domain;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.DataAccess.ValueResolvers
{
    public class ChildrenListResolver<TChild> : ISitecoreResolver, IValueResolver
       where TChild : ISitecoreDomainEntity
    {
        public object Resolve(Item item)
        {
            Type genericParameterType = GetType().GetGenericArguments()[0];

            return CreateLazyList(genericParameterType, () =>
            {
                var repository = new ReadOnlyRepository();
                IMap map = MapFinder.FindMap(genericParameterType);
                IList list = CreateEmptyTypedList(genericParameterType);
                foreach (Item child in item.Children)
                {
                    if (child.DescendsFromTemplate(map.TemplatePath))
                    {
                        var entity = repository.Get<TChild>(child);
                        list.Add(entity);
                    }
                }
                return list;
            });
        }


        private static IList CreateLazyList(Type genericParameterType, Func<IEnumerable> loader)
        {
            return Activator.CreateInstance(typeof(LazyList<>).MakeGenericType(genericParameterType), loader) as IList;
        }

        private static IList CreateEmptyTypedList(Type argType)
        {
            return Activator.CreateInstance(typeof(List<>).MakeGenericType(argType)) as IList;
        }

        #region IValueResolver Members

        public bool CanResolve(Type type)
        {
            return false;
        }

        public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
            throw new NotImplementedException();
        }

        public object ResolveItemFieldValue(object rawValue)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
