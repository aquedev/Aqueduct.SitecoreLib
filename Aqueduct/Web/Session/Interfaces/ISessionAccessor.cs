namespace Aqueduct.Web.Session.Interfaces
{
    public interface ISessionAccessor
    {
        bool ContainsKey(string key);
        void Add<T>(string key, T value);
        T Get<T>(string key);
    }
}
