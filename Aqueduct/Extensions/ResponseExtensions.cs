using System.Web;

namespace Aqueduct.Extensions
{
    /// <summary>
    /// Class containing extension methods for the System.Web.HttpResponse class
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Perform a permanent (301) redirection to the url provided
        /// </summary>
        public static void RedirectPermanent (this HttpResponse response, string url)
        {
            response.Redirect (url, false);
            response.StatusCode = 301;
            response.StatusDescription = "Permanently moved";
            response.End ();
        }
    }
}
