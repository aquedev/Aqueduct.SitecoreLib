using Sitecore.Data;
using System;
using Sitecore.Data.Items;
using System.Collections.Generic;
namespace Aqueduct.SitecoreLib
{
    public class TreeCrawler
    {
        private readonly string _startPath;
        private readonly Database _database;

        public TreeCrawler(string startPath, Database database)
        {
            _database = database;
            _startPath = startPath;
        }

        public void Crawl(Action<Item> actionOnItem, Predicate<Item> shouldUseItem)
        {
            Item startItem = _database.GetItem(_startPath);

            if (startItem == null)
                throw new InvalidOperationException("No item exists at path: " + _startPath);

            Queue<Item> queue = new Queue<Item>();
            queue.Enqueue(startItem);

            while(queue.Count > 0)
            {
                var item = queue.Dequeue();
                
                if (item.HasChildren)
                    foreach (Item child in item.Children)
                        queue.Enqueue(child);

                if(shouldUseItem(item))
                    actionOnItem(item);
            }
        }
    }
}

