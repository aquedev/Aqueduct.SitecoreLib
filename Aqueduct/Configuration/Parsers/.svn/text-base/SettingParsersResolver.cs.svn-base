using System;
using System.Collections.Generic;

namespace Aqueduct.Configuration.Parsers
{
    public class SettingParsersResolver
    {
        List<ISettingParser> _parsers = new List<ISettingParser>();

        public SettingParsersResolver()
        {
            
        }

        public void Register(ISettingParser parser)
        {
            ConfigGuard.ArgumentNotNull(parser, "parser", "Cannot add a null parser");
            _parsers.Add(parser);
        }

        public void Register(IEnumerable<ISettingParser> parsers)
        {
            foreach (var parser in parsers)
                Register(parser);
        }

        public ISettingParser Resolve(Type parsedType)
        {
            ConfigGuard.ArgumentNotNull(parsedType, "parsedType", "Parsed type cannot be null");

            foreach (var parser in _parsers)
            {
                if (parser.CanParse(parsedType))
                    return parser;
            }

            throw new ConfigurationException("No parser was found for type of " + parsedType.Name);
        }
    }
}
