using System;
using System.Collections.Generic;
using Aqueduct.Domain;
using StructureMap;

namespace Aqueduct.SitecoreLib.DataAccess
{
    [Serializable]
    public class EntityCache
    {
        private const string Key = "EntityCacheKey";

        private readonly Dictionary<EntityUniqueKey, ISitecoreDomainEntity> m_innerCache = new Dictionary<EntityUniqueKey, ISitecoreDomainEntity>();
        
        private static readonly IContextLevelCache m_contextCache = ObjectFactory.GetInstance<IContextLevelCache>();

        public bool Contains(Guid id, Type entityType)
        {
            return m_innerCache.ContainsKey(new EntityUniqueKey(id, entityType));
        }

        public ISitecoreDomainEntity Get(Guid id, Type entityType)
        {
            return m_innerCache[new EntityUniqueKey(id, entityType)];
        }

        public void Add(ISitecoreDomainEntity entity)
        {
            var key = new EntityUniqueKey(entity.Id, entity.GetType());

            if (!m_innerCache.ContainsKey(key))
            {
                m_innerCache.Add(key, entity);
            }
        }

        private EntityCache()
        {
        }

        public static bool Exists
        {
            get { return GetCache () != null; }
        }

        public static EntityCache Current
        {
            get
            {
                var entityCache = GetCache ();
                if (entityCache == null)
                {
                    entityCache = new EntityCache ();
                    SetCache (entityCache);
                }
                return entityCache;
            }
        }

        public void Clear()
        {
            m_innerCache.Clear ();
        }

        private static void SetCache (EntityCache scope)
        {
            m_contextCache.Store (Key, scope);
        }

        private static EntityCache GetCache ()
        {
            return m_contextCache.Get<EntityCache> (Key);
        }
    }

    public interface IContextLevelCache
    {
        void Store<T>(string key, T t) where T : class;
        T Get<T>(string key) where T : class;
        void Remove (string key);
    }
}
