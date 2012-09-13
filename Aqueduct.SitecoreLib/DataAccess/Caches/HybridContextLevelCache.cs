using System.Web;

namespace Aqueduct.SitecoreLib.DataAccess.Caches
{
    public class HybridContextLevelCache : IContextLevelCache
    {
        private readonly IContextLevelCache m_httpContextLevelCache = new HttpContextLevelCache();
        private readonly IContextLevelCache m_callContextLevelCache = new CallContextLevelCache();

        private IContextLevelCache InnerContextLevelCache
        {
            get { return HttpContext.Current == null ? m_callContextLevelCache : m_httpContextLevelCache; }
        }

        public void Store<T>(string key, T t) where T : class
        {
            InnerContextLevelCache.Store(key, t);
        }

        public T Get<T>(string key) where T : class
        {
            return InnerContextLevelCache.Get<T>(key);
        }

        public void Remove(string key)
        {
            InnerContextLevelCache.Remove(key);
        }
    }
}
