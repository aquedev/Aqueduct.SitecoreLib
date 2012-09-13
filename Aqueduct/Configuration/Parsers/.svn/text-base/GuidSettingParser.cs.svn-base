using System;
using Aqueduct.Extensions;

namespace Aqueduct.Configuration.Parsers
{
    public class GuidSettingParser : ISettingParser
    {
        public object Parse(string raw, Type settingType)
        {
            if (raw.IsNullOrEmpty())
                return Guid.Empty;
            return new Guid(raw);
        }

        public bool CanParse(Type settingType)
        {
            return settingType == typeof(Guid);
        }
    }
}
