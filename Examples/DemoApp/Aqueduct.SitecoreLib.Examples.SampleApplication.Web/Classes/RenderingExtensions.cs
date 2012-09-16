using System;
using System.Linq;
using System.Text;
using System.Web;
using Aqueduct.Domain;
using Aqueduct.SitecoreLib.Domain;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Classes
{
    public static class RenderingExtensions
    {
        public static string GetImage<T>(this T entity, Func<T, Image> expression) where T : class
        {
            return GetImage(entity, expression, 0, string.Empty, string.Empty);
        }

        public static string GetImage<T>(this T entity, Func<T, Image> expression, int width) where T : class
        {
            return GetImage(entity, expression, width, string.Empty, string.Empty);
        }

        public static string GetImage<T>(this T entity, Func<T, Image> expression, string cssClass) where T : class
        {
            return GetImage(entity, expression, 0, cssClass, string.Empty);
        }

        public static string GetImage<T>(this T entity, Func<T, Image> expression, string cssClass, string dataAttributes) where T : class
        {
            return GetImage(entity, expression, 0, cssClass, dataAttributes);
        }

        public static string GetImage<T>(this T entity, Func<T, Image> expression, int width, string cssClass, string dataAttributes) where T : class
        {
            if (entity != null)
            {
                var image = expression.Invoke(entity);

                if (image != null && !string.IsNullOrEmpty(image.Url))
                {
                    string newSrc;

                    if (width > 0)
                        newSrc = string.Format("{0}?mw={1}", image.Url, width);
                    else
                        newSrc = string.Format("{0}", image.Url);

                    var sb = new StringBuilder(string.Format("<img src=\"{0}\"", newSrc));
                    if (!string.IsNullOrEmpty(image.Alt))
                        sb.Append(string.Format(" alt=\"{0}\"", HttpUtility.HtmlEncode(image.Alt)));
                    if (!string.IsNullOrEmpty(cssClass))
                        sb.Append(string.Format(" class=\"{0}\"", cssClass));
                    if (!string.IsNullOrEmpty(dataAttributes))
                        sb.Append(string.Format(" {0}", dataAttributes));
                    sb.Append("/>");

                    return sb.ToString();
                }
            }

            return string.Empty;
        }

        public static string GetAnchorImage(this Image image, string cssClass, string dataAttributes)
        {
            return GetAnchorImage(image, x => x, 0, cssClass, dataAttributes);
        }

        public static string GetAnchorImage(this Image image, string cssClass)
        {
            return GetAnchorImage(image, x => x, 0, cssClass, null);
        }

        public static string GetAnchorImage<T>(this T entity, Func<T, Image> expression, int width, string cssClass, string dataAttributes) where T : class
        {
            if (entity != null)
            {
                var image = expression.Invoke(entity);

                if (image != null && !string.IsNullOrEmpty(image.Url))
                {
                    string newSrc = "";

                    if (width > 0)
                        newSrc = string.Format("{0}?mw={1}", image.Url, width);
                    else
                        newSrc = string.Format("{0}?mw={1}", image.Url, image.Width);

                    var sb = new StringBuilder(string.Format("<a href=\"{0}\"", newSrc));
                    if (!string.IsNullOrEmpty(cssClass))
                        sb.Append(string.Format(" class=\"{0}\"", cssClass));
                    if (!string.IsNullOrEmpty(dataAttributes))
                        sb.Append(string.Format(" {0}", dataAttributes));
                    if (!string.IsNullOrEmpty(image.Alt))
                        sb.Append(string.Format(">{0}</a>", HttpUtility.HtmlEncode(image.Alt)));
                    else
                        sb.Append("></a>");

                    return sb.ToString();
                }
            }

            return string.Empty;
        }

        public static string GetImage(this Image image)
        {
            return GetImage(image, x => x, 0);
        }

        public static string GetImage(this Image image, int width)
        {
            return GetImage(image, x => x, width);
        }

        public static string GetImage(this Image image, string cssClass)
        {
            return GetImage(image, x => x, 0, cssClass, string.Empty);
        }

        public static string GetImage(this Image image, string cssClass, string dataAttributes)
        {
            return GetImage(image, x => x, 0, cssClass, dataAttributes);
        }

        public static string GetImage(this Image image, int width, string cssClass, string dataAttributes)
        {
            return GetImage(image, x => x, width, cssClass, dataAttributes);
        }

        public static string BeginAnchorTag(this Link link)
        {
            if (link.IsValidLink())
            {
                return !string.IsNullOrEmpty(link.Target)
                           ? string.Format("<a href='{0}' target='{1}'>", link.Url, link.Target)
                           : string.Format("<a href='{0}'>", link.Url);
            }

            return string.Empty;
        }

        public static string BeginAnchorTag(this Link link, string cssClass)
        {
            if (link.IsValidLink())
            {
                return !string.IsNullOrEmpty(link.Target)
                           ? string.Format("<a href='{0}' target='{1}' class='{2}'>", link.Url, link.Target, cssClass)
                           : string.Format("<a href='{0}' class='{1}'>", link.Url, cssClass);
            }

            return string.Empty;
        }

        public static string EndAnchorTag(this Link link)
        {
            return link.IsValidLink() ? "</a>" : string.Empty;
        }

        public static string RendorAnchorTag(this Link link)
        {
            return RenderAnchorTag(link, string.Empty);
        }

        public static string RenderAnchorTag(this Link link, string cssClass)
        {
            if (!link.IsValidLink()) return string.Empty;

            var beginTag = string.IsNullOrEmpty(cssClass) ? BeginAnchorTag(link) : BeginAnchorTag(link, cssClass);

            return string.Format("{0}{1}</a>", beginTag, link.Caption);
        }

        public static bool IsValidLink(this Link link)
        {
            return link != null && !string.IsNullOrEmpty(link.Url);
        }

        public static string BoldFirstWord(this string original)
        {
            if (string.IsNullOrEmpty(original)) return original;

            var split = original.Split(null);
            if (split.Any())
            {
                var firstWord = split.First();

                return original.Replace(firstWord, string.Format(@"<strong>{0}</strong>", firstWord));
            }

            return original;
        }
    }
}