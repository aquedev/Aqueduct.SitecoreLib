using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Aqueduct.SitecoreLib.Search.Constants;

namespace Aqueduct.SitecoreLib.Search.DynamicFields
{
   public class LockDateField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Locking.IsLocked())
         {
            LockField lockField = item.Fields[FieldIDs.Lock];
            return lockField.Date.ToString(IndexConstants.DateTimeFormat);
         }

         return string.Empty;
      }
   }
}
