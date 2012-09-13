using System;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public class ConcreteTypeNeededEventArgs : EventArgs
    {
        public Type ConcreteType { get; set; }
        public Item Item { get; private set; }

        public ConcreteTypeNeededEventArgs(Item item)
        {
            Item = item;
        }
    }
}