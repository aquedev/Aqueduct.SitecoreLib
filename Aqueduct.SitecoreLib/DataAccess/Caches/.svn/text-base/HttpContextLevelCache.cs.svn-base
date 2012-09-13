using System.Collections;
using System.Web;

namespace Aqueduct.SitecoreLib.DataAccess.Caches
{
    public class HttpContextLevelCache : IContextLevelCache
    {
        public void Store<T>(string key, T t)
            where T : class
        {
            ContextItems[key] = t;
        }

        public void Remove(string key)
        {
            if (ContextItems.Contains(key))
            {
                ContextItems.Remove(key);
            }
        }

        public T Get<T>(string key)
            where T : class
        {
            return ContextItems[key] as T;
        }

        private static IDictionary ContextItems
        {
            get { return HttpContext.Current.Items; }
        }
    }
}
