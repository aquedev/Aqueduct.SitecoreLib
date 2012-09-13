using System.Runtime.Remoting.Messaging;

namespace Aqueduct.SitecoreLib.DataAccess.Caches
{
    public class CallContextLevelCache : IContextLevelCache
    {
        public void Store<T>(string key, T t)
            where T : class
        {
            CallContext.SetData(key, t);
        }

        public void Remove(string key)
        {
            CallContext.FreeNamedDataSlot(key);
        }

        public T Get<T>(string key)
            where T : class
        {
            return CallContext.GetData(key) as T;
        }
    }
}
