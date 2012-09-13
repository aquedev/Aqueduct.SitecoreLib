using System;

namespace Aqueduct.Caching
{
	public class NullCache<TKey, TValue> : ICache<TKey, TValue>
		where TValue : class
	{
		public TValue Get(TKey key)
		{
			return null;
		}

		public bool ContainsKey(TKey key)
		{
			return false;
		}

		public void Remove(TKey key)
		{
		}

		public void Add(TKey key, TValue t)
		{
		}

		public void Add(TKey key, TValue t, TimeSpan duration)
		{
		}
	}
}