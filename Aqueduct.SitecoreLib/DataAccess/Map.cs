using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using Aqueduct.Utils;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public abstract class Map<TDomainEntity> : IMap 
    {
        private readonly List<MapEntry> m_mappings;

        protected Map()
        {
            m_mappings = new List<MapEntry>();
        }

        public abstract string TemplatePath { get; }

        public IList<MapEntry> Mappings
        {
            get { return m_mappings; }
        }

        protected void CopyMappingsFrom (IMap map)
        {
            m_mappings.AddRange (map.Mappings);
        }

        protected MapEntry MapProperty<TProperty>(Expression<Func<TDomainEntity, TProperty>> expression)
        {
            Guard.ParameterNotNull(expression, "expression");

            PropertyInfo property = GetPropertyInfo(expression);

            var mapEntry = new MapEntry(property);
            m_mappings.Add(mapEntry);

            return mapEntry;
        }

        //TODO: MG to be moved to BRC.DataAccess
        public string GetFormsTemplatePath (string partialPath)
        {
            return Path.Combine("BritishRedCross/BRC/Forms/", partialPath);
        }

        private static PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TDomainEntity, TProperty>> expression)
        {
            var propertyAccessExpression = (expression.Body as MemberExpression);

            if (propertyAccessExpression == null)
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");

            return propertyAccessExpression.Member as PropertyInfo;
        }

        public Type MappedEntityType
        {
            get { return typeof(TDomainEntity); }
        }

        public virtual bool SkipTemplateChecking
        {
            get { return false; }
        }

    }
}
