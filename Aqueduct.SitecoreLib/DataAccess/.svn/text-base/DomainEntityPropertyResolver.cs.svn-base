using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Aqueduct.Diagnostics;
using Aqueduct.Domain;
using Aqueduct.Extensions;
using Aqueduct.SitecoreLib.DataAccess.ValueResolvers;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public class DomainEntityPropertyResolver
    {
        private static readonly List<IValueResolver> m_resolvers = GetResolverList();

        public event EventHandler<ConcreteTypeNeededEventArgs> ConcreteTypeNeeded = (sender, args) => { };

        public static void AddValueResolver(IValueResolver valueResolver)
        {
            m_resolvers.Insert(0, valueResolver);
        }

        public static void ResolveItem<TDomainEntity>(Item item, Map<TDomainEntity> map, TDomainEntity entity)
            where TDomainEntity : ISitecoreDomainEntity
        {
            foreach (MapEntry mapEntry in map.Mappings.Where(m => m.HasResolver == false))
            {
                object rawValue = mapEntry.MappedProperty.GetValue(entity, null);
                object resolvedValue = ResolveItemFieldValue(mapEntry.MappedProperty, rawValue);

                if (resolvedValue != null)
                    item[mapEntry.MappedTo] = resolvedValue.ToString();
            }
        }

        public ISitecoreDomainEntity ResolveEntity(Item item, IMap map)
        {
            Type mappedType = map.MappedEntityType;

            if (mappedType.IsAbstract)
            {
                return ResolveEntityToConcreteType(item, map, mappedType);
            }

            return ResolveEntity(item, map, mappedType);
        }

        #region Methods marked internal for Unit Testing

        internal static object ResolveEntityPropertyValue(PropertyInfo mappedProperty, string rawValue)
        {
            Debug.Write(mappedProperty.Name);
            return ResolveEntityValue(mappedProperty.PropertyType, rawValue);
        }

        internal static object ResolveItemFieldValue(PropertyInfo mappedProperty, object rawValue)
        {
            return GetResolver(mappedProperty.PropertyType).ResolveItemFieldValue(rawValue);
        }

        #endregion


        private ISitecoreDomainEntity ResolveEntityToConcreteType(Item item, IMap map, Type abstractType)
        {
            Type concreteType = GetConcreteType(item);

            if (concreteType == null)
            {
                throw FormatException("No concrete type supplied for mapped abstract type ({0})", abstractType.Name);
            }

            if (!abstractType.IsAssignableFrom(concreteType))
            {
                throw FormatException("[{0}] cannot be supplied as an implementation of abstract class [{1}]",
                                       concreteType.Name, abstractType.Name);
            }

            return ResolveEntity(item, map, concreteType);
        }

        private static List<IValueResolver> GetResolverList()
        {
            var resolvers = new List<IValueResolver>();
            resolvers.Add(new NullableResolver());
            resolvers.Add(new ConvertibleValueResolver());
            resolvers.Add(new BooleanValueResolver());
            resolvers.Add(new DateValueResolver());
            resolvers.Add(new EnumValueResolver());
            resolvers.Add(new ImageValueResolver());
            resolvers.Add(new FileValueResolver());
            resolvers.Add(new DomainEntityValueResolver());
            resolvers.Add(new DomainEntityListResolver(new DomainEntityRepository()));
            resolvers.Add(new GuidValueResolver());
            resolvers.Add(new ValuesListResolver(resolvers));
            resolvers.Add(new LinkValueResolver());
            resolvers.Add(new DefaultValueResolver()); // this should always be last
            return resolvers;
        }

        private static IValueResolver GetResolver(Type type)
        {
            return m_resolvers.First(resolver => resolver.CanResolve(type));
        }

        private static ISitecoreDomainEntity ResolveEntity(Item item, IMap map, Type concreteType)
        {
            if (item == null)
            {
                return null;
            }

            if (!map.SkipTemplateChecking && !item.DescendsFromTemplate(map.TemplatePath))
            {
                Logger.LogWarningMessage(String.Format("Item ({0}, {1}) cannot be resolved to type ({2}) because it doesn't descend from template {3}",
                    item.ID, item.Name, concreteType.FullName, map.TemplatePath));
                return null;
            }

            Logger.LogDebugMessage(String.Format("Resolving item ({0}, {1}) of type {2}",
                item.ID, item.Name, concreteType.FullName));

            EntityCache entityCache = EntityCache.Current;

            if (entityCache.Contains(item.ID.Guid, concreteType))
            {
                Logger.LogDebugMessage(String.Format("Resolving item from Cache: ({0}, {1}) of type {2}", item.ID, item.Name, concreteType.FullName));
                DataAccessStatistics.IncrementResolveEntityFromCache();
                return entityCache.Get(item.ID.Guid, concreteType);
            }

            var domainEntity = (ISitecoreDomainEntity)Activator.CreateInstance(concreteType);

            // write Id
            domainEntity.Id = item.ID.Guid;

            //Add the entity to the Resolver scope
            entityCache.Add(domainEntity);

            // write ParentId
            domainEntity.ParentId = item.ParentID.Guid;

            // write Url
            domainEntity.Url = GetItemUrl(item);

            // write properties
            foreach (MapEntry mapEntry in map.Mappings)
            {
                PropertyInfo property = mapEntry.MappedProperty;
                if (mapEntry.HasResolver)
                {
                    object value = mapEntry.Resolver.Resolve(item);
                    property.SetValue(domainEntity, value, null);
                }
                else
                {
                    string rawValue = item[mapEntry.MappedTo];
                    if (rawValue.IsNullOrEmpty() && mapEntry.DontSetIfEmptyProperty)
                        continue;

                    object value = GetPropertyValueFromItem(item, mapEntry, property);
                    property.SetValue(domainEntity, value, null);
                }
            }

            DataAccessStatistics.IncrementResolveEntity();
            return domainEntity;
        }

        private static string GetItemUrl(Item item)
        {
            return MediaManager.HasMediaContent(item)
                       ? MediaManager.GetMediaUrl(item)
                       : LinkManager.GetItemUrl(item);
        }

        private Type GetConcreteType(Item item)
        {
            var args = new ConcreteTypeNeededEventArgs(item);
            ConcreteTypeNeeded(this, args);
            return args.ConcreteType;
        }

        private static object GetPropertyValueFromItem(BaseItem item, MapEntry mapEntry, PropertyInfo property)
        {
            string rawValue = item[mapEntry.MappedTo];

            if (string.IsNullOrEmpty(rawValue) && mapEntry.HasDefaultValue)
            {
                return mapEntry.DefaultValue;
            }
            try
            {
                return ResolveEntityPropertyValue(property, rawValue);
            }
            catch (Exception ex)
            {
                string message = string.Format("Error setting property {0} with value \"{1}\"", property.Name, rawValue);
                throw new Exception(message, ex);
            }
        }

        private static object ResolveEntityValue(Type propertyType, string rawValue)
        {
            return GetResolver(propertyType).ResolveEntityPropertyValue(rawValue, propertyType);
        }

        private static Exception FormatException(string format, params object[] args)
        {
            string message = String.Format(format, args);
            return new Exception(message);
        }
        private static ILogger Logger
        {
            get { return DataAccessLogger.Logger; }
        }
    }
}