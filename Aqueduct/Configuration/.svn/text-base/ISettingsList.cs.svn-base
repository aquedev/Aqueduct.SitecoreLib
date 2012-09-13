using System.Collections.Generic;

namespace Aqueduct.Configuration
{
    public interface ISettingsList : IDictionary<string, Setting>
    {
        Dictionary<string, object> ToKeyValueDictionary ();
        void Add (Setting setting);
        void AddSettings (ISettingsList settings);
        ISettingsList ToReadOnly ();
    }
}