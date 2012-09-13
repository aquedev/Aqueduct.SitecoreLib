namespace Aqueduct.Caching
{
	public interface ICacheFactory
	{
		ICache<TKey, TValue> GetCache<TKey, TValue> ()
			where TValue : class;
	}
}
