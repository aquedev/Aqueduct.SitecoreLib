using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using Aqueduct.Extensions;

namespace Aqueduct.Domain
{
    [Serializable]
    public class Link
    {
        public Link Clone()
        {
            return new Link() { Url = Url };
        }

        public Link()
        {
            LinkType = LinkTypes.None;
            Url = String.Empty;
            Domain = String.Empty;
            Path = String.Empty;
            Protocol = String.Empty;
            Parameters = new SafeDictionary();
            Target = String.Empty;
            CssClass = String.Empty;
        }

        public LinkTypes LinkType { get; set; }
        public Guid Id { get; set; }
        public string Target { get; set; }

        public string Url
        {
            get { return CreateUrl(); }
            set { ParseUrl(value); }
        }
        public string Path { get; set; }
        public string Domain { get; set; }
        public string Protocol { get; set; }
        public string Port { get; set; }
        public string CssClass { get; set; }
        public string JavasciptUrl { get; set; }

        private string CreateUrl()
        {
            if (LinkType == LinkTypes.JavaScript)
                return JavasciptUrl;

            int index = 0;

			var parameters = new string[Parameters.Count];
            foreach (var key in Parameters.Keys)
            {
				parameters[index] = String.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(Parameters[key]));
            	index++;
            }

        	string url = Protocol + Domain + Port + Path;
				
            if (parameters.Length > 0)
				url = string.Format("{0}?{1}", url, string.Join("&", parameters));

            return url;
        }

        private void ParseUrl(string value)
        {
            if (value.IsNullOrEmpty())
                return;

            DetermineLinkType(value);

            if(LinkType == LinkTypes.JavaScript)
            {
                JavasciptUrl = value;
                return;
            }

            if (value.StartsWith("?"))
            {
                ParseParameters(value.Substring(1));
            }
            else
            {
                string[] parts = value.Split(new[] {'?'}, StringSplitOptions.RemoveEmptyEntries);
                string urlToProccess = value;
                if (parts.Length > 1)
                {
                    urlToProccess = ParseProtocol(parts[0]);
                    ParseParameters(parts[1]);
                }

                if (value.StartsWith("mailto:"))
                {
                    Path = ParseDomain(value);
                }
                else
                {
                    urlToProccess = ParseProtocol(urlToProccess);
                    urlToProccess = ParseDomain(urlToProccess);
                    urlToProccess = ParsePort(urlToProccess);
                    urlToProccess = ParsePath(urlToProccess);

                    if (Parameters.Count > 0 && Path.IsNullOrEmpty())
                        Path = "/";
                }
            }
        }

        private void DetermineLinkType(string rawUrl)
        {
            if(LinkType == LinkTypes.None)
            {
                if(rawUrl.StartsWith("javascript:"))
                    LinkType = LinkTypes.JavaScript;
                if (rawUrl.StartsWith("mailto:"))
                    LinkType = LinkTypes.MailTo;
            }
        }

        private string ParsePort(string url)
        {
            const string portPattern = @"^:[0-9]+";
            Match match = Regex.Match(url.Trim(), portPattern);
            if (match.Success)
            {
                Port = match.Value;
                url = Regex.Replace(url.Trim(), portPattern, "");
            }
            return url;
        }

        private string ParseProtocol(string url)
        {
            const string protocolPattern = @"https?://";
            Match match = Regex.Match(url.Trim(), protocolPattern);
            if (match.Success)
            {
                Protocol = match.Value;
                return Regex.Replace(url.Trim(), protocolPattern, "");
            }
            return url;
        }

        private string ParseDomain(string url)
        {
            const string domainPattern = @"^[-A-z0-9.]+";
            Match match = Regex.Match(url.Trim(), domainPattern);
            if (match.Success)
            {
                Domain = match.Value;
                return Regex.Replace(url.Trim(), domainPattern, "");
            }
            return url;
        }

        private string ParsePath(string url)
        {
            if (url.Trim().Length == 0)
            {
                Path = "/";
                return "";
            }
            const string pathPattern = @"^/[-A-z0-9+&@#/%=~_|!:,.;\s]*";
            Match match = Regex.Match(url.Trim(), pathPattern);
            if (match.Success)
            {
                Path = match.Value;
                return Regex.Replace(url.Trim(), pathPattern, "");
            }
            return url;
        }

        private void ParseParameters(string parametersString)
        {
            if (parametersString.IsNotEmpty())
            {
                string[] pairs = parametersString.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string pair in pairs)
                {
                    string[] nameValue = pair.Split('=');
                    if (nameValue.Length == 2)
                        Parameters[nameValue[0]] = HttpUtility.UrlDecode(nameValue[1]);
                }
            }
        }

        public string Caption { get; set; }

        public static bool IsNullOrEmpty(Link link)
        {
            return (link == null) ||
                (link.LinkType == LinkTypes.None &&
                    String.IsNullOrEmpty(link.Url));
        }

        public IDictionary<string, string> Parameters { get; private set; }

        public override string ToString()
        {
            return Url;
        }

        public class SafeDictionary : IDictionary<string, string>
        {
            private Dictionary<string, string> m_inner = new Dictionary<string, string>();

            public void Add(string key, string value)
            {
                m_inner[key] = value;
            }

            public bool ContainsKey(string key)
            {
                return m_inner.ContainsKey(key);
            }

            public ICollection<string> Keys
            {
                get { return m_inner.Keys; }
            }

            public bool Remove(string key)
            {
                return m_inner.Remove(key);
            }

            public bool TryGetValue(string key, out string value)
            {
                return m_inner.TryGetValue(key, out value);
            }

            public ICollection<string> Values
            {
                get { return m_inner.Values; }
            }

            public string this[string key]
            {
                get { return m_inner[key]; }
                set { m_inner[key] = value; }
            }

            public void Add(KeyValuePair<string, string> item)
            {
                m_inner[item.Key] = item.Value;
            }

            public void Clear()
            {
                m_inner.Clear();
            }

            public bool Contains(KeyValuePair<string, string> item)
            {
                return ((ICollection<KeyValuePair<string,string>>)m_inner).Contains(item);
            }

            public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
            {
                ((ICollection<KeyValuePair<string, string>>)m_inner).CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return ((ICollection<KeyValuePair<string, string>>)m_inner).Count; }
            }

            public bool IsReadOnly
            {
                get { return ((ICollection<KeyValuePair<string, string>>)m_inner).IsReadOnly; }
            }

            public bool Remove(KeyValuePair<string, string> item)
            {
                return ((ICollection<KeyValuePair<string, string>>)m_inner).Remove(item);
            }

            public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            {
                return ((IEnumerable<KeyValuePair<string, string>>)m_inner).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)m_inner).GetEnumerator();
            }
        }
    }
}
