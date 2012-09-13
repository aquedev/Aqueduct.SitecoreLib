using System;
using System.Web;

namespace Aqueduct.Caching
{
	public class StandardWebCache<TKey, TValue> : ICache<TKey, TValue>
		where TValue : class
	{
		public TValue Get(TKey key)
		{
			return HttpRuntime.Cache[key.ToString()] as TValue;
		}

		public bool ContainsKey(TKey key)
		{
			return null != HttpRuntime.Cache[key.ToString()];
		}

		public void Remove(TKey key)
		{
			HttpRuntime.Cache.Remove(key.ToString());
		}

		public void Add(TKey key, TValue t)
		{
			HttpRuntime.Cache.Insert(key.ToString(), t);
		}

		public void Add(TKey key, TValue t, TimeSpan duration)
		{
			HttpRuntime.Cache.Insert(key.ToString(), t, null, DateTime.Now.Add(duration), TimeSpan.Zero);
		}
	}
}