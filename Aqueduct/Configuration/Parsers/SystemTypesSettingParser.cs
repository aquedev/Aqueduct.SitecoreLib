using System;

namespace Aqueduct.Configuration.Parsers
{
    public class SystemTypesSettingParser : ISettingParser
    {
        public object Parse(string raw, Type settingType)
        {
            if (settingType == typeof(string))
                return raw;

            return Convert.ChangeType(raw, settingType);
        }

        public bool CanParse(Type settingType)
        {
            return settingType != null 
                && settingType != typeof(bool) //Boolean types get special handling
                && typeof(IConvertible).IsAssignableFrom(settingType);
        }
    }
}
