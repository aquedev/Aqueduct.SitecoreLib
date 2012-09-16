using System;
using Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.DataAccess.Resolvers
{
    class EnumResolver<T> : ISitecoreResolver where T : struct
    {
        private readonly string m_fieldName;

        public EnumResolver(string fieldName)
        {
            m_fieldName = fieldName;
        }

        public object Resolve(Item item)
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException("T must be an Enum");
            }

            var name = string.Empty;

            if (item != null)
            {
                var linkField = (InternalLinkField)item.Fields[m_fieldName];
                if (linkField != null && linkField.TargetItem != null)
                    name = linkField.TargetItem.Name;
            }

            T result;
            Enum.TryParse(name, out result);

            return result;
        }
    }
}
