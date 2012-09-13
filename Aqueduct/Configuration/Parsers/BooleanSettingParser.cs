using System;

namespace Aqueduct.Configuration.Parsers
{
    public class BooleanSettingParser : ISettingParser
    {
        #region ISettingParser Members

        public bool CanParse(Type settingType)
        {
            return settingType == typeof(bool);
        }

        public object Parse(string raw, Type settingType)
        {
            switch (raw.ToLower())
            {
                case "true":
                case "on":
                case "1":
                case "yes":
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
}
