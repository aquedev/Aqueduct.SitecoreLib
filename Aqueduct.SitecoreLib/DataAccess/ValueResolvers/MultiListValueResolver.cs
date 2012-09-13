using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aqueduct.Common;
using Aqueduct.Diagnostics;
using Aqueduct.Domain;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.DataAccess.ValueResolvers
{
    public class MultiListValueResolver<T> : ISitecoreResolver
        where T : ISitecoreDomainEntity
    {
        private readonly int? m_maxItems;
        private const char Delimiter = '|';
        protected readonly ReadOnlyRepository Repository = new ReadOnlyRepository();
        private readonly string m_multiListFieldName;

        public MultiListValueResolver(string fieldName)
        {
            m_multiListFieldName = fieldName;
        }

        public MultiListValueResolver(string fieldName, int maxItems)
            : this(fieldName)
        {
            m_maxItems = maxItems;
        }

        #region ISitecoreResolver Members

        public object Resolve(Item item)
        {
            var genericParameterType = GetType().GetGenericArguments()[0];
            return CreateLazyList(genericParameterType, () => GetList(item, genericParameterType));
        }

        #endregion

        private IList GetList(BaseItem item, Type genericParameterType)
        {
            var list = CreateEmptyTypedList(genericParameterType);
            if (item.Fields[m_multiListFieldName] != null)
            {
                var listIds = item.Fields[m_multiListFieldName].Value.Split(Delimiter);

                if (m_maxItems.HasValue)
                    listIds = listIds.Take(m_maxItems.Value).ToArray();

                foreach (var id in listIds)
                {
                    if (string.IsNullOrEmpty(id)) continue;

                    try
                    {
                        GetEntity(id, list);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError("Error getting item from multilist", ex);
                    }
                }
            }

            return list;
        }

        protected virtual void GetEntity(string id, IList list)
        {
            var entity = Repository.Get<T>(new Guid(id));

            list.Add(entity);
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
