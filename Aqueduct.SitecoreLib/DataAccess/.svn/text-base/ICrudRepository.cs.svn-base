
using System;
using Aqueduct.Domain;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public interface ICrudRepository
    {
        TEntity Create<TEntity>(Guid parentId, TEntity entity, string itemName) where TEntity : class, ISitecoreDomainEntity;
        TEntity Create<TEntity>(string contentPath, TEntity entity) where TEntity : class, ISitecoreDomainEntity;
        TEntity Create<TEntity>(Item parentItem, TEntity entity, string itemName) where TEntity : class, ISitecoreDomainEntity;
        TEntity Create<TEntity, TParentEntity>(TParentEntity parentEntity, TEntity entity, string itemName)
            where TEntity : class, ISitecoreDomainEntity
            where TParentEntity : class, ISitecoreDomainEntity;

        TEntity Read<TEntity>(Guid id) where TEntity : class, ISitecoreDomainEntity;
        TEntity Read<TEntity>(string contentPath) where TEntity : class, ISitecoreDomainEntity;
        TEntity Read<TEntity>(Item item) where TEntity : class, ISitecoreDomainEntity;

        TEntity Update<TEntity>(TEntity entity) where TEntity : class, ISitecoreDomainEntity;

        void Delete<TEntity>(Guid id) where TEntity : class, ISitecoreDomainEntity;
        void Delete<TEntity>(string contentPath) where TEntity : class, ISitecoreDomainEntity;
        void Delete<TEntity>(Item item) where TEntity : class, ISitecoreDomainEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : class, ISitecoreDomainEntity;
    }

    public class CrudRepository : ICrudRepository
    {
        private readonly DomainEntityPropertyResolver m_resolver;

        public CrudRepository()
        {
            m_resolver = new DomainEntityPropertyResolver();
        }

        public CrudRepository(string databaseName)
            : this()
        {
            m_specificDatabase = Factory.GetDatabase(databaseName);
        }

        private readonly Database m_specificDatabase;
        protected Database CurrentDatabase
        {
            get
            {
                if (m_specificDatabase != null)
                    return m_specificDatabase;

                // this is used by code that executes under the sitecore editor UI, such as item event handlers
                if (Context.Database.Name == DatabaseNames.Core)
                    return Context.ContentDatabase;

                return Context.Database;
            }
        }

        protected Database MasterDatabase
        {
            get { return Factory.GetDatabase("master"); }
        }

        private TEntity SaveItem<TEntity>(Item item, Map<TEntity> map, TEntity entity) where TEntity : ISitecoreDomainEntity
        {
            item.Editing.BeginEdit();

            DomainEntityPropertyResolver.ResolveItem(item, map, entity);

            item.Editing.EndEdit();

            return (TEntity)m_resolver.ResolveEntity(item, map);
        }

        #region Create
        public TEntity Create<TEntity>(Guid parentId, TEntity entity, string itemName) where TEntity : class, ISitecoreDomainEntity
        {
            var parentItem = MasterDatabase.GetItem(new ID(parentId));

            return Create(parentItem, entity, itemName);
        }

        public TEntity Create<TEntity>(string contentPath, TEntity entity) where TEntity : class, ISitecoreDomainEntity
        {
            var map = MapFinder.FindMap<TEntity>();
            var templateItem = MasterDatabase.GetTemplate(map.TemplatePath);

            using (new SecurityDisabler())
            {
                var newItem = MasterDatabase.CreateItemPath(contentPath, templateItem);

                return SaveItem(newItem, map, entity);
            }
        }

        public TEntity Create<TEntity>(Item parentItem, TEntity entity, string itemName) where TEntity : class, ISitecoreDomainEntity
        {
            var newPath = string.Format("{0}/{1}", parentItem.Paths.FullPath, itemName);

            return Create(newPath, entity);
        }

        public TEntity Create<TEntity, TParentEntity>(TParentEntity parentEntity, TEntity entity, string itemName)
            where TEntity : class, ISitecoreDomainEntity
            where TParentEntity : class, ISitecoreDomainEntity
        {
            var parentItem = MasterDatabase.GetItem(new ID(parentEntity.Id));

            return Create(parentItem, entity, itemName);
        }
        #endregion

        #region Read
        public TEntity Read<TEntity>(Guid id) where TEntity : class, ISitecoreDomainEntity
        {
            var item = CurrentDatabase.GetItem(new ID(id));

            return Read<TEntity>(item);
        }

        public TEntity Read<TEntity>(string contentPath) where TEntity : class, ISitecoreDomainEntity
        {
            var item = CurrentDatabase.GetItem(contentPath);

            return Read<TEntity>(item);
        }

        public TEntity Read<TEntity>(Item item) where TEntity : class, ISitecoreDomainEntity
        {
            var map = MapFinder.FindMap<TEntity>();

            return (TEntity)m_resolver.ResolveEntity(item, map);
        }
        #endregion

        #region Update
        public TEntity Update<TEntity>(TEntity entity) where TEntity : class, ISitecoreDomainEntity
        {
            var map = MapFinder.FindMap<TEntity>();

            using (new SecurityDisabler())
            {
                var item = MasterDatabase.GetItem(new ID(entity.Id));

                return SaveItem(item, map, entity);
            }
        }
        #endregion

        #region Delete
        public void Delete<TEntity>(Guid id) where TEntity : class, ISitecoreDomainEntity
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(string contentPath) where TEntity : class, ISitecoreDomainEntity
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(Item item) where TEntity : class, ISitecoreDomainEntity
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, ISitecoreDomainEntity
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
