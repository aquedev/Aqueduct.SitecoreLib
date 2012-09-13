using System;
using System.Xml.Linq;
using Aqueduct.Domain;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Resources.Media;

namespace Aqueduct.SitecoreLib
{
	public static class LinkUrl
	{
        private static string GetAttributeValue(XElement e, XName name)
        {
            XAttribute attribute = e.Attribute(name);
            
            return attribute == null 
                ? String.Empty 
                : attribute.Value;
        }

	    internal static Link GetUrl (XElement field, Database database)
		{
			string attribute = GetAttributeValue(field, "linktype");
			string url = GetAttributeValue(field, "url");
			string caption = GetAttributeValue(field, "text");
			string itemId = GetAttributeValue(field, "id");
			string anchor = GetAttributeValue(field, "anchor");
			string queryString = GetAttributeValue(field, "querystring");
            string target = GetAttributeValue(field, "target");
            string cssClass = GetAttributeValue(field, "class");

            var link = new Link { Caption = caption, Target = target, CssClass = cssClass };
	        if (!string.IsNullOrEmpty(anchor))
			{
				anchor = "#" + anchor;
			}
			switch (attribute)
			{
				case "anchor":
					link.LinkType = LinkTypes.Anchor;
					link.Url = anchor;
					break;
				case "external":
					link.LinkType = LinkTypes.External;
					link.Url = GetExternalUrl(url);
					break;
				case "javascript":
					link.LinkType = LinkTypes.JavaScript;
					link.Url = GetJavaScriptUrl(url);
					break;
				case "mailto":
					link.LinkType = LinkTypes.MailTo;
					link.Url = GetMailToLink(url);
					break;
				case "media":
					link.LinkType = LinkTypes.Media;
					link.Url = GetMediaUrl(database, itemId);
					break;
                default: // assume "internal"
                    link.LinkType = LinkTypes.Internal;
                    link.Url = GetInternalUrl(database, url, itemId, anchor, queryString);
                    break;
            }
			return link;
		}

		private static string GetExternalUrl(string url)
		{
			Assert.ArgumentNotNull(url, "url");
			if (!url.StartsWith("/") && (url.IndexOf("://") < 0))
			{
				url = "http://" + url;
			}
			return url;
		}

		private static string GetInternalUrl(Database database, string url, string itemId, string anchor, string queryString)
		{
			Assert.ArgumentNotNull(database, "database");
			Assert.ArgumentNotNull(url, "url");
			Assert.ArgumentNotNull(itemId, "itemId");
			Assert.ArgumentNotNull(anchor, "anchor");
			Assert.ArgumentNotNull(queryString, "queryString");
			Item item = database.Items[url] ?? database.Items[itemId];
			if (item == null)
			{
				return string.Empty;
			}
			if (item.Paths.IsMediaItem)
			{
				return GetMediaUrl(database, itemId);
			}
			return (LinkManager.GetItemUrl(item) + anchor + GetQueryString(queryString));
		}

		private static string GetJavaScriptUrl(string url)
		{
			Assert.ArgumentNotNull(url, "url");
			if (!url.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
			{
				url = "javascript:" + url;
			}
			return (url + ";return false;");
		}

		private static string GetMailToLink(string url)
		{
			Assert.ArgumentNotNull(url, "url");
			if (!url.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase))
			{
				url = "mailto:" + url;
			}
			return url;
		}

		private static string GetMediaUrl(Database database, string itemId)
		{
			Assert.ArgumentNotNull(database, "database");
			Assert.ArgumentNotNull(itemId, "itemId");
			Item item = database.Items[itemId];
			if (item == null)
			{
				return string.Empty;
			}
			string mediaUrl = MediaManager.GetMediaUrl(item);
			if (mediaUrl.StartsWith("~/media/"))
			{
				mediaUrl = "/" + mediaUrl;
			}
			return Assert.ResultNotNull(mediaUrl);
		}

		private static string GetQueryString(string queryString)
		{
			Assert.ArgumentNotNull(queryString, "queryString");
			if (!string.IsNullOrEmpty(queryString))
			{
				return ("?" + queryString);
			}
			return queryString;
		}
	}
}