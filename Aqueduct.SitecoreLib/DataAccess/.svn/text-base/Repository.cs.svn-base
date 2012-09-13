using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Aqueduct.Diagnostics;
using Aqueduct.Domain;
using Aqueduct.Extensions;
using Aqueduct.Utils;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.SecurityModel;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public interface IRepository<TMemberEntity> where TMemberEntity : class, ISitecoreDomainEntity
    {
        TMemberEntity GetEntity(Guid id);
        TMemberEntity GetEntityWithChildren(Guid id, Action<TMemberEntity, Guid> actionOnChild);

        TMemberEntity GetEntityWithChildren<TChild>(Guid id, Action<TMemberEntity, TChild> actionOnChild)
            where TChild : ISitecoreDomainEntity;

        void ProcessEntityChildren(TMemberEntity entity, Action<TMemberEntity, Guid> actionOnChild);

        void ProcessEntityChildren<TChild>(TMemberEntity entity, Action<TMemberEntity, TChild> actionOnChild)
            where TChild : ISitecoreDomainEntity;

        void ProcessEntityChildren<TParent, TChild>(TParent entity, Action<TParent, TChild> actionOnChild)
            where TChild : ISitecoreDomainEntity
            where TParent : ISitecoreDomainEntity;

        void SaveEntity(TMemberEntity entity);
        void CreateItem(string itemPath, TMemberEntity entity);
        void CreateItem<TEntity>(string itemPath, TEntity entity) where TEntity : ISitecoreDomainEntity;
        void SaveItem(Item item, TMemberEntity entity);
        void SaveItem<TEntity>(Item item, TEntity entity) where TEntity : ISitecoreDomainEntity;
        Item GetItem(string path);
        MediaItem GetMediaItem(string path);
        TemplateItem GetTemplateItem(string path);
        TemplateItem GetTemplateItem(Guid templateId);
        TMemberEntity ParseItem(Item item);
        TMemberEntity ParseItem(Guid itemId);

        TEntity ParseItem<TEntity>(Item item)
            where TEntity : ISitecoreDomainEntity;

        TEntity ParseItemUsingInferredTemplatePath<TEntity>(Item item)
            where TEntity : ISitecoreDomainEntity;

        ISitecoreDomainEntity ParseItem(Item item, IMap map);

        TEntity ParseItemUsingInferredTemplatePath<TEntity>(Item item, Repository<TMemberEntity>.MapNotFoundBehaviour mapNotFoundBehaviour)
            where TEntity : ISitecoreDomainEntity;

        TEntity ParseItemUsingTemplatePath<TEntity>(Item item, string templatePath)
            where TEntity : ISitecoreDomainEntity;

        ISitecoreDomainEntity GetEntityOfType(Type entityType, Guid id);

        TEntity GetEntity<TEntity>(Guid id)
            where TEntity : ISitecoreDomainEntity;

        Link GetLink(Item item);
        Template GetTemplate(IMap map);
        Guid GetItemParentId(Guid itemId);
        string GetItemParentUrl(Guid itemId);
    }

    public class Repository<TMemberEntity> : IRepository<TMemberEntity> where TMemberEntity : class, ISitecoreDomainEntity
    {
        static ILogger Logger
        {
            get { return DataAccessLogger.Logger; }
        }

        public enum MapNotFoundBehaviour
        {
            ReturnNullEntity, Continue
        }

        private readonly DomainEntityPropertyResolver m_resolver;

        public Repository()
        {
            m_resolver = new DomainEntityPropertyResolver();
            m_resolver.ConcreteTypeNeeded += Resolver_ConcreteTypeNeeded;
        }

        public Repository(string databaseName)
            : this()
        {
            m_specificDatabase = Factory.GetDatabase(databaseName);
        }

        private void Resolver_ConcreteTypeNeeded(object sender, ConcreteTypeNeededEventArgs args)
        {
            Type concreteType = GetConcreteFieldTypeFromItem(args.Item);
            if (concreteType != null)
            {
                args.ConcreteType = concreteType;
            }
        }

        protected virtual Type GetConcreteFieldTypeFromItem(Item item)
        {
            return null;
        }

        private static Database m_specificDatabase;
        protected static Database CurrentDatabase
        {
            get
            {
                if (m_specificDatabase != null)
                    return m_specificDatabase;

                // this is used by code that executes under the sitecore editor UI, such as item event handlers
                if (Context.Database != null && Context.Database.Name == DatabaseNames.Core)
                    return Context.ContentDatabase;

                if (Context.Database != null)
                    return Context.Database;

                return WebDatabase;
            }
        }

        protected static Database MasterDatabase
        {
            get { return Factory.GetDatabase("master"); }
        }

        protected static Database WebDatabase
        {
            get { return Factory.GetDatabase("web"); }
        }

        public virtual TMemberEntity GetEntity(Guid id)
        {
            Item item = GetItem(id);

            return ParseItem<TMemberEntity>(item);
        }

        public TMemberEntity GetEntityWithChildren(Guid id, Action<TMemberEntity, Guid> actionOnChild)
        {
            Item item = GetItem(id);
            if (item == null)
                return null;
            var entity = ParseItem<TMemberEntity>(item);
            if (item.HasChildren)
            {
                foreach (Item childItem in item.Children)
                {
                    actionOnChild(entity, childItem.ID.Guid);
                }
            }
            return entity;
        }

        public TMemberEntity GetEntityWithChildren<TChild>(Guid id, Action<TMemberEntity, TChild> actionOnChild)
            where TChild : ISitecoreDomainEntity
        {
            Item item = GetItem(id);
            var entity = ParseItem<TMemberEntity>(item);
            ProcessEntityChildren<TChild>(entity, actionOnChild);
            return entity;
        }

        public void ProcessEntityChildren(TMemberEntity entity, Action<TMemberEntity, Guid> actionOnChild)
        {
            Item item = GetItem(entity.Id);

            if (item.HasChildren)
            {
                foreach (Item childItem in item.Children)
                {
                    actionOnChild(entity, childItem.ID.Guid);
                }
            }
        }

        public void ProcessEntityChildren<TChild>(TMemberEntity entity, Action<TMemberEntity, TChild> actionOnChild)
            where TChild : ISitecoreDomainEntity
        {
            ProcessEntityChildren<TMemberEntity, TChild>(entity, actionOnChild);
        }

        public void ProcessEntityChildren<TParent, TChild>(TParent entity, Action<TParent, TChild> actionOnChild)
            where TChild : ISitecoreDomainEntity
            where TParent : ISitecoreDomainEntity
        {
            Item item = GetItem(entity.Id);

            Map<TChild> map = MapFinder.FindMap<TChild>();

            if (map != null && item.HasChildren)
            {
                var domainEntityRepository = new DomainEntityRepository();
                Guid[] childIds = Array.ConvertAll(item.Children.ToArray(), x => x.ID.Guid);
                IList<ISitecoreDomainEntity> childrenOfType = domainEntityRepository.GetEntities(childIds, map);
                foreach (ISitecoreDomainEntity childOfType in childrenOfType)
                {
                    actionOnChild(entity, (TChild)childOfType);
                }
            }
        }

        public void SaveEntity(TMemberEntity entity)
        {
            Item item = GetItem(entity.Id);

            SaveItem(item, entity);
        }

        public void CreateItem(string itemPath, TMemberEntity entity)
        {
            CreateItem<TMemberEntity>(itemPath, entity);
        }

        public void CreateItem<TEntity>(string itemPath, TEntity entity) where TEntity : ISitecoreDomainEntity
        {
            Map<TEntity> map = MapFinder.FindMap<TEntity>();

            TemplateItem templateItem = GetTemplateItem(map.TemplatePath);

            using (new SecurityDisabler())
            {
                Item newItem = MasterDatabase.CreateItemPath(itemPath, templateItem);

                SaveItem(newItem, map, entity);
            }
        }

        public void CreateItem<TEntity>(string itemName, ISitecoreDomainEntity parentEntity, TEntity entity) where TEntity : ISitecoreDomainEntity
        {
            var parentItem = GetItem(parentEntity.Id);
            var itemPath = string.Format("{0}/{1}", parentItem.Paths.FullPath, itemName);

            CreateItem(itemPath, entity);
        }

        public void SaveItem(Item item, TMemberEntity entity)
        {
            SaveItem<TMemberEntity>(item, entity);
        }

        public void SaveItem<TEntity>(Item item, TEntity entity) where TEntity : ISitecoreDomainEntity
        {
            Map<TEntity> map = MapFinder.FindMap<TEntity>();

            using (new SecurityDisabler())
            {
                SaveItem(item, map, entity);
            }
        }

        private static void SaveItem<TEntity>(Item item, Map<TEntity> map, TEntity entity) where TEntity : ISitecoreDomainEntity
        {
            item.Editing.BeginEdit();

            DomainEntityPropertyResolver.ResolveItem(item, map, entity);

            item.Editing.EndEdit();
        }

        public static void UpdateProperty<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression) where TEntity : ISitecoreDomainEntity
        {
            Guard.ParameterNotNull(entity);
            Guard.ParameterNotNull(propertyExpression);

            IMap entityMap = MapFinder.FindMap<TEntity>() as IMap;
            string propertyName = propertyExpression.GetPropertyName();

            Logger.LogDebugMessage(String.Format("Updating property '{0}' of '{1}' for item {2}", propertyName, typeof(TEntity), entity.Id));

            MapEntry mapEntry = entityMap.Mappings.First(mapping => mapping.MappedProperty.Name == propertyName);


            using (new SecurityDisabler())
            {
                Item item = GetItem(entity.Id);

                item.Editing.BeginEdit();
                PropertyInfo mappedProperty = mapEntry.MappedProperty;
                object resolvedValue = DomainEntityPropertyResolver.ResolveItemFieldValue(mappedProperty, mappedProperty.GetValue(entity, null));
                item.Fields[mapEntry.MappedTo].Value = resolvedValue.ToString();
                item.Editing.EndEdit();
            }
        }

        public static Item GetItem(Guid id)
        {
            return GetItem(id, CurrentDatabase);
        }

        public static Item GetItem(Guid id, Language language)
        {
            return GetItem(id, CurrentDatabase, language);
        }

        public static Item GetItem(Guid id, Database database)
        {
            return database.GetItem(new ID(id));
        }

        public static Item GetItem(Guid id, Database database, Language language)
        {
            return database.GetItem(new ID(id), language);
        }

        public Item GetItem(string path)
        {
            return CurrentDatabase.GetItem(path);
        }

        public MediaItem GetMediaItem(string path)
        {
            return GetItem(path);
        }

        public TemplateItem GetTemplateItem(string path)
        {
            return CurrentDatabase.GetTemplate(path);
        }

        public TemplateItem GetTemplateItem(Guid templateId)
        {
            return CurrentDatabase.GetTemplate(new ID(templateId));
        }

        public TMemberEntity ParseItem(Item item)
        {
            return ParseItem<TMemberEntity>(item);
        }

        public TMemberEntity ParseItem(Guid itemId)
        {
            return ParseItem(GetItem(itemId));
        }

        public TEntity ParseItem<TEntity>(Item item)
            where TEntity : ISitecoreDomainEntity
        {
            Map<TEntity> map = MapFinder.FindMap<TEntity>();

            if (map == null)
                return default(TEntity);

            return (TEntity)ParseItem(item, map);
        }

        public static string GetTemplatePath<TEntity>()
            where TEntity : ISitecoreDomainEntity
        {
            Map<TEntity> map = MapFinder.FindMap<TEntity>();
            return map.TemplatePath;
        }

        public TEntity ParseItemUsingInferredTemplatePath<TEntity>(Item item)
            where TEntity : ISitecoreDomainEntity
        {
            return ParseItemUsingInferredTemplatePath<TEntity>(item, MapNotFoundBehaviour.Continue);
        }

        public ISitecoreDomainEntity ParseItem(Item item, IMap map)
        {
            return m_resolver.ResolveEntity(item, map);
        }

        public TEntity ParseItemUsingInferredTemplatePath<TEntity>(Item item, MapNotFoundBehaviour mapNotFoundBehaviour)
            where TEntity : ISitecoreDomainEntity
        {
            string templatePath = item.Template.FullName;
            IMap map = MapFinder.FindMap(templatePath);
            if (map == null && mapNotFoundBehaviour == MapNotFoundBehaviour.ReturnNullEntity)
            {
                return default(TEntity);
            }
            return (TEntity)m_resolver.ResolveEntity(item, map);
        }

        public TEntity ParseItemUsingTemplatePath<TEntity>(Item item, string templatePath)
            where TEntity : ISitecoreDomainEntity
        {
            IMap map = MapFinder.FindMap(templatePath);
            return (TEntity)m_resolver.ResolveEntity(item, map);
        }

        public ISitecoreDomainEntity GetEntityOfType(Type entityType, Guid id)
        {
            Item item = GetItem(id);

            if (item != null)
            {
                IMap map = MapFinder.FindMap(entityType);
                if (map != null)
                {
                    return m_resolver.ResolveEntity(item, map);
                }
            }

            return null;
        }

        public TEntity GetEntity<TEntity>(Guid id)
            where TEntity : ISitecoreDomainEntity
        {
            return (TEntity)GetEntityOfType(typeof(TEntity), id);
        }

        public Link GetLink(Item item)
        {
            var link = new Link();

            if (item != null)
            {
                link.LinkType = LinkTypes.Internal;
                link.Caption = item.Name;
                link.Url = LinkManager.GetItemUrl(item);
            }

            return link;
        }

        public Template GetTemplate(IMap map)
        {
            return TemplateManager.GetTemplate(map.TemplatePath, CurrentDatabase);
        }

        public Guid GetItemParentId(Guid itemId)
        {
            return GetItem(itemId).Parent.ID.Guid;
        }

        public string GetItemParentUrl(Guid itemId)
        {
            return GetItem(itemId).Parent.GetItemUrl();
        }
    }
}