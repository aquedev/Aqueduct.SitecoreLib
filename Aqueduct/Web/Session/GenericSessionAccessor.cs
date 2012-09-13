using System.Web;
using Aqueduct.Web.Session.Interfaces;
namespace Aqueduct.Web.Session
{
    public class GenericSessionAccessor : ISessionAccessor
    {
        public bool ContainsKey(string key)
        {
            if(HttpContext.Current.Session == null)
            {
                return false;
            }

            return HttpContext.Current.Session[key] != null;
        }

        public void Add<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] == null)
                return default(T);
            return (T)HttpContext.Current.Session[key];
        }
    }
}
