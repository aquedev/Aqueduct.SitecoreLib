using System;

namespace Aqueduct.Caching
{
	public interface ICache<TKey, TValue>
		where TValue : class
	{
		TValue Get(TKey key);
		bool ContainsKey(TKey key);
		void Remove(TKey key);
		void Add(TKey key, TValue t);
		void Add(TKey key, TValue t, TimeSpan duration);
	}
}
