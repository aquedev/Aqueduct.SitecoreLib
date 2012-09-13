using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;

namespace Aqueduct.Web
{
    public class CookieManager
    {
        public static void AddCookie(string key, string value, DateTime? expiryDate)
        {
            AddCookie(key, value, expiryDate, null);
        }

        public static void AddCookie(string key, string value, DateTime? expiryDate, string domain)
        {
            HttpCookie httpCookie = new HttpCookie(key, value);
            if (expiryDate.HasValue)
                httpCookie.Expires = expiryDate.Value;
            if (!String.IsNullOrEmpty(domain))
                httpCookie.Domain = domain;

            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }

        public static void AddCookie(string key, Dictionary<string,string> values, DateTime? expiryDate)
        {
            AddCookie(key, values, expiryDate, null);
        }

        public static void AddCookie(string key, Dictionary<string,string> values, DateTime? expiryDate, string domain)
        {
            HttpCookie httpCookie = new HttpCookie(key);

            foreach (KeyValuePair<string, string> value in values)
                httpCookie.Values.Add(value.Key, value.Value);

            if (expiryDate.HasValue)
                httpCookie.Expires = expiryDate.Value;
            if (!String.IsNullOrEmpty(domain))
                httpCookie.Domain = domain;

            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }

        public static string GetCookieValue(string key)
        {
            return IsCookieExist(key) ? HttpContext.Current.Request.Cookies[key].Value : string.Empty;
        }

        public static Dictionary<string, string> GetCookieValues(string key)
        {
            Dictionary<string, string> cookieValues = null;

            if (IsCookieExist(key))
            {
                cookieValues = new Dictionary<string, string>();
                NameValueCollection cookies = HttpContext.Current.Request.Cookies[key].Values;

                foreach (string cookieValue in cookies)
                    cookieValues.Add(cookieValue, cookies[cookieValue]);
            }

            return cookieValues;
        }

        public static void DeleteCookie(string key)
        {
            DeleteCookie(key, null);
        }

        public static void DeleteCookie(string key, string domain)
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[key];
            if (httpCookie != null)
            {
                httpCookie.Expires = DateTime.Now.AddDays(-1);
                if (!String.IsNullOrEmpty(domain))
                    httpCookie.Domain = domain;
                
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
        }

        public static bool IsCookieExist(string key)
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[key];
            return httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value);
        }

        public static void AddSecureCookie(string key, string value, DateTime? expiryDate)
        {
        	DateTime ticketExpiryDate = expiryDate.HasValue ? expiryDate.Value : DateTime.Now.AddMinutes(10);
            FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(1, key, DateTime.Now,
																								ticketExpiryDate, true, value);

            AddCookie(key, FormsAuthentication.Encrypt(formsAuthenticationTicket), expiryDate);
        }

        public static string GetSecureCookieValue(string key)
        {
            FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthentication.Decrypt(GetCookieValue(key));
            return formsAuthenticationTicket.UserData;
        }
    }
}
