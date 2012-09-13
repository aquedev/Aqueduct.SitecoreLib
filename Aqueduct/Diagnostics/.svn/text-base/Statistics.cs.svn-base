using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Aqueduct.Diagnostics
{
    public static class Statistics
    {
        private static IEnumerable<IStatisticsStore> m_stores = new List<IStatisticsStore>();
        private static IEnumerable<IStatisticsStore> ActiveStores
        {
            get { return m_stores.Where(store => store.IsActive()); }
        }

        public static void Initialize()
         {
            m_stores = new Collection<IStatisticsStore> { new StaticStatisticsStore() };
        } 

        public static void Initialize(IEnumerable<IStatisticsStore> stores)
        {
            m_stores = stores;
        }

        public static void IncrementValue(string name)
        {
            IncrementValue(name, 1);
        }

        public static void IncrementValue(string name, long value)
        {
            try
            {

                foreach (var store in ActiveStores)
                    store.Increment(name, value);
            }
            catch (Exception ex)
            {
                AppLogger.LogFatalError(String.Format("Error while incrementing {0}", name), ex);                
            }
        }

        public static IList<StatisticsResults> GetStatistics()
        {
            List<StatisticsResults> stores = new List<StatisticsResults>();

            try
            {
                foreach (var store in ActiveStores)
                {
                    stores.Add(new StatisticsResults(store.GetType().Name, store.GetStatistics()));
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogFatalError("Error while retrieving statistics", ex);
            }

            return stores;
        }
    }
}
