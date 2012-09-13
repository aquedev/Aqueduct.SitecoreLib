using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace Aqueduct.Helpers
{
    public static class XmlExtensions
    {
        [Obsolete("Use the xml extensions in Aqueduct.Extensions instead")]
        public static string AttributeValue(this XElement node, string attrName)
        {
            if (node == null)
                return string.Empty;

            var attr = node.Attribute(attrName);
            return attr != null ? attr.Value : string.Empty;
        }

        [Obsolete("Use the xml extensions in Aqueduct.Extensions instead")]
        public static string AttributeValue(this XmlNode node, string attrName)
        {
            if (node == null)
                return string.Empty;

            XmlNode attr = node.Attributes.GetNamedItem(attrName);
            return attr != null ? attr.Value : string.Empty;
        }

        [Obsolete("Use the xml extensions in Aqueduct.Extensions instead")]
        public static string InnerXml(this XElement element)
        {
            if (element == null)
                return String.Empty;

            return element.Nodes().Aggregate("", (str, node) => str + node.ToString());;
        }
    }
}
