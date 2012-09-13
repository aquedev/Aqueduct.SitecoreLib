using System.Web;

namespace Aqueduct.Web.Session.Interfaces
{
    public interface ICookieAccessor
    {
        HttpCookie GetCookie(string cookieName);
        void SetCookie(HttpCookie cookie);
    }
}
