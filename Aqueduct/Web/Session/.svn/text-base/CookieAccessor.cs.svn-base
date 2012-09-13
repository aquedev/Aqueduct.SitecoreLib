using System.Web;
using Aqueduct.Web.Session.Interfaces;

namespace Aqueduct.Web.Session
{
    public class CookieAccessor : ICookieAccessor
    {
        #region Implementation of ICookieAccessor

        public HttpCookie GetCookie(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName];
        }

        public void SetCookie(HttpCookie cookie)
        {
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion
    }
}
