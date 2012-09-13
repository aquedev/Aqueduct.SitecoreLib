using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Configuration;
using Sitecore.Data;
using System.Linq;

namespace Aqueduct.SitecoreLib.ItemFinderCriteria
{
    public class SitecoreItemFinder
    {
        private readonly Database m_database;
        public const string RootNodePath = "/sitecore";

        readonly Queue<Item> m_itemsToBeProcessed = new Queue<Item>();

        /// <summary>
        /// Initializes a new instance of the SitecoreItemFinder class.
        /// </summary>
        public SitecoreItemFinder(string databaseName)
        {
            m_database = Factory.GetDatabase(databaseName);
        }

        public List<Item> GetAllItems(IItemFinderCriterion criteria)
        {
            return GetAllItems(RootNodePath, criteria);
        }

        public List<Item> GetAllItems(string startPath, IItemFinderCriterion criteria)
        {
            EnqueueItems(m_database.SelectItems(startPath));
            var results = new List<Item>();

            while(m_itemsToBeProcessed.Count > 0)
            {
                Item currentlyProcessedItem = m_itemsToBeProcessed.Dequeue();

                if(currentlyProcessedItem.HasChildren)
                {
                    EnqueueItems(currentlyProcessedItem.Children.ToArray());
                }

                if (criteria.Match(currentlyProcessedItem))
                {
                    results.Add(currentlyProcessedItem);
                }
            } 

            return results;
        }

        public List<Item> GetMainSections()
        {
            Item rootItem = m_database.SelectSingleItem(RootNodePath);
            return rootItem.Children.ToArray().ToList();
        }

        private void EnqueueItems(Item[] items)
        {
            foreach (Item item in items)
            {
                m_itemsToBeProcessed.Enqueue(item);
            }
        }
        
    }
}


