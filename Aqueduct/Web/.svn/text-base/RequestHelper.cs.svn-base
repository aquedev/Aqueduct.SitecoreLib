using System.Collections.Generic;
using System.Web;
using System.Net;
using Aqueduct.Extensions;
using Aqueduct.Net;

namespace Aqueduct.Web
{

    /// <summary>
    /// Static helper methods to do with Request
    /// </summary>
    public class RequestHelper
    {
        private static HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }

        /// <summary>
        /// Get user's external IP address. It checks HTTP_X_FORWARDED_FOR, HTTP_CLIENT_IP and REMOTE_ADDR for an external IP
        /// </summary>
        /// <returns>user's external Ip address</returns>
        public static string GetUsersExternalIp()
        {
        	if (IsValidExternalIp(GetXForwardedAddress()))
                return GetXForwardedAddress();
            else if (IsValidExternalIp(GetClientIp()))
                return GetClientIp();
            else
                return Request.UserHostAddress;
        }
        
        private static string GetClientIp()
        {
            return Request.ServerVariables["HTTP_CLIENT_IP"];
        }

        private static string GetXForwardedAddress()
        {
            string clientip = null;
            var xff = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(xff))
            {
                string[] parts = xff.Split(',');
                if (parts.Length > 0)
                {
                    clientip = parts[0].Trim();
                }
            }
            return clientip;
        }

        private static bool IsValidExternalIp(string ip)
        {
            if (ip.IsNullOrEmpty())
                return false;

            IPAddress address = IPAddress.None;
            if (IPAddress.TryParse(ip, out address) == false)
                return false;

            List<IPAddressRange> localIPRanges = new List<IPAddressRange> { 
                new IPAddressRange(IPAddress.Parse("10.0.0.0"), IPAddress.Parse("10.255.255.255")),
            new IPAddressRange(IPAddress.Parse("172.16.0.0"), IPAddress.Parse("172.31.255.255")),
            new IPAddressRange(IPAddress.Parse("192.168.0.0"), IPAddress.Parse("192.168.255.255")),
            new IPAddressRange(IPAddress.Parse("169.254.0.0"), IPAddress.Parse("169.254.255.255"))
            };

            foreach (var range in localIPRanges)
            {
                if (range.IsInRange(address))
                    return false;
            }

            return true;
        }
    }
}
