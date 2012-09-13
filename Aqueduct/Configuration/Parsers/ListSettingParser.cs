using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Aqueduct.Extensions;

namespace Aqueduct.Configuration.Parsers
{
    public class ListSettingParser : ISettingParser
    {
        private readonly SettingParsersResolver _resolver;

        public ListSettingParser(SettingParsersResolver resolver)
        {
            _resolver = resolver;
        }

        public bool CanParse(Type settingType)
        {
            return settingType.IsGenericType 
                && settingType.GetGenericTypeDefinition() == typeof(IList<>);
        }

        public object Parse(string raw, Type settingType)
        { 
            Type[] typeGenericArgs = settingType.GetGenericArguments();
            IList list = Activator.CreateInstance(typeof(List<>).MakeGenericType(typeGenericArgs)) as IList;

            if (!raw.IsNullOrEmpty())
            {
                string wrappedRaw = String.Format("<root>{0}</root>", raw);
                XDocument doc = XDocument.Parse(wrappedRaw);
                var items = from item in doc.Root.Descendants()
                            select item.Value;

                ISettingParser elementParser = GetElementParser(settingType, typeGenericArgs[0]);
                foreach (string item in items)
                {
                    list.Add(elementParser.Parse(item, typeGenericArgs[0]));
                }
            }

            return CreateReadOnlyCollection(typeGenericArgs, list);
        }

        private ISettingParser GetElementParser(Type settingType, Type genericTypePart)
        {
            ISettingParser elementParser = _resolver.Resolve(genericTypePart);
            if (elementParser == null)
                throw new ArgumentException("Cannot find a ISettingParser to parse the element of generic list " + settingType.FullName);
            return elementParser;
        }

        private static object CreateReadOnlyCollection(Type[] genericArguments, IList list)
        {
            return Activator.CreateInstance(typeof(ReadOnlyCollection<>).MakeGenericType(genericArguments), list);
        }
    }
}
