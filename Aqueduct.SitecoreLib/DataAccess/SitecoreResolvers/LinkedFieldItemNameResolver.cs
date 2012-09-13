using System;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers
{
    public class LinkedFieldItemNameResolver : ISitecoreResolver
    {
        private readonly string m_linkFieldName;

        public LinkedFieldItemNameResolver (string linkFieldName)
        {
            m_linkFieldName = linkFieldName;
        }

        public object Resolve(Item item)
        {
            Field field = item.Fields[m_linkFieldName];
            
            if (field == null)
            {
                throw new Exception (String.Format ("Field name \"{0}\" not found", m_linkFieldName));
            }

            try
            {
                var id = new Guid(field.Value);
            	Item linkedItem = Sitecore.Context.Database.GetItem(new ID(id));
				return linkedItem.Name;
            }
            catch(FormatException)
            {
                throw new Exception(String.Format("Field name \"{0}\" in item \"{1}\" does not link to another item", m_linkFieldName, item.Name));
            }

        }
    }
}