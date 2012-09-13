using System;
using System.Collections.Generic;

namespace Aqueduct.Configuration
{
    public interface IConfigurationHandler
    {
        event EventHandler SettingsChanged;
        string this [string key] { get; }
        ISettingsList Settings { get; }
        string Get (string key);
        string Get (string key, string defaultValue);
        T Get<T> (string key);
        T Get<T> (string key, T defaultValue);
        Dictionary<string, object> GetAll (Func<string, bool> keyFunction);
    }
}