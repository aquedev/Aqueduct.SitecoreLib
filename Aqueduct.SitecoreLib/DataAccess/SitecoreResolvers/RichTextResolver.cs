using Sitecore.Data.Items;
using Sitecore.Links;

namespace Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers
{
    public class RichTextResolver : ISitecoreResolver
    {
        private readonly string _fieldName;

        public RichTextResolver(string fieldName)
        {
            _fieldName = fieldName;
        }

        public object Resolve(Item item)
        {
            return LinkManager.ExpandDynamicLinks(item.Fields[_fieldName].Value);
        }
    }
}
