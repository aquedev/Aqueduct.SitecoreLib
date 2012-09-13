﻿using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Aqueduct.SitecoreLib.Search.DynamicFields
{
   public class LockUserNameField : BaseDynamicField
   {
      public override string ResolveValue(Item item)
      {
         Assert.ArgumentNotNull(item, "item");

         if (item.Locking.IsLocked())
         {
            LockField lockField = item.Fields[FieldIDs.Lock];
            return lockField.Owner.ToLowerInvariant();
         }

         return string.Empty;
      }

   }
}
