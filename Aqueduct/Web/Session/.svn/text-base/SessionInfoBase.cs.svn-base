using System;
using Aqueduct.Web.Session.Interfaces;

namespace Aqueduct.Web.Session
{
    public class SessionInfo 
    {
        private readonly ISessionAccessor m_sessionAccessor;
        private const string SessionKeyPrefix = "aque_session__";

        public SessionInfo(ISessionAccessor sessionAccessor)
        {
            m_sessionAccessor = sessionAccessor;
        }

        public void StoreInSession<T>(string sessionValueKey, T t)
        {
            string key = string.Concat(SessionKeyPrefix, sessionValueKey);
            m_sessionAccessor.Add(key, t);
        }

        public T GetFromSession<T>(string sessionValueKey)
            where T : class, new()
        {
            return GetFromSession(sessionValueKey, () => new T());
        }

        public T GetFromSession<T>(string sessionValueKey, Func<T> createT)
        {
            string key = string.Concat(SessionKeyPrefix, sessionValueKey);

            if (m_sessionAccessor.ContainsKey(key))
            {
                return m_sessionAccessor.Get<T>(key);
            }
            var x = createT();
            m_sessionAccessor.Add(key, x);
            return x;
        }
    }
}
