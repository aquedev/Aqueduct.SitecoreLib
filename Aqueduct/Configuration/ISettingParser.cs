using System;

namespace Aqueduct.Configuration
{
    public interface ISettingParser
    {
        bool CanParse(Type settingType);
        object Parse (string raw, Type settingType);
    }
}