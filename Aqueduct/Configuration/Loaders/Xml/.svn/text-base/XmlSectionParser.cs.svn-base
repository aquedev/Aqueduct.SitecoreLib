using System.Xml.Linq;
using System.Linq;
using Aqueduct.Common;
using Aqueduct.Extensions;

namespace Aqueduct.Configuration.Loaders.Xml
{
    public class XmlSectionParser
    {
        public static Section Parse(XElement section)
        {
            if (section == null)
                return new Section ("", ApplicationMode.Disabled, new string[0]);

            var name = section.AttributeValue ("name");
            ApplicationMode mode = section.AttributeValue("mode").ToEnum(ApplicationMode.Auto);
            string[] serverNames = ParseServerNames (section);

            return new Section(name, mode, serverNames);
        }

        private static string[] ParseServerNames (XElement section)
        {
            XNamespace sectionNameSpace = section.GetDefaultNamespace();
            var servers = from serverNode in section.Descendants(sectionNameSpace + "server")
                          where serverNode.Attribute("name").Value.IsNotEmpty()
                          select serverNode.Attribute("name").Value;

            return servers.ToArray();
        }
    }
}