using Sitecore.Data.Items;
using Aqueduct.SitecoreLib.Search.Utilities;

namespace Aqueduct.SitecoreLib.Search.DynamicFields
{
   public abstract class BaseDynamicField : SearchField
   {
      public abstract string ResolveValue(Item item);

      public string FieldKey { get; set; }
   }
}
