using System;
using System.Web.UI;
using Aqueduct.Utils;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using Sitecore.Data;

namespace Aqueduct.SitecoreLib
{
    public static class SublayoutHelper
    {
        public static Control GetControl(Guid sublayoutId, Item currentItem)
        {
            Guard.AssertThat(sublayoutId != null && sublayoutId != Guid.Empty, "SublayoutId cannot be an empty Guid");
            Guard.AssertThat(currentItem != null, "Cannot get sublayout for a null item");

            var sublayoutItem = GetSublayoutItem(sublayoutId);
            if (sublayoutItem != null)
            {
                var sublayout = new Sublayout();
                sublayout.Path = sublayoutItem.FilePath;
                sublayout.DataSource = currentItem.Paths.FullPath;
                return sublayout.GetUserControl();
            }
            return null;
        }

        private static SublayoutItem GetSublayoutItem(Guid sublayoutId)
        {
            return new SublayoutItem(Databases.CurrentDatabase.GetItem(new ID(sublayoutId)));
        }
    }
}
