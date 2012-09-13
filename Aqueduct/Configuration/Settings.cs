using System.Collections.Generic;

namespace Aqueduct.Configuration
{
    public class SettingsList : Dictionary<string, Setting>, ISettingsList
    {
        #region ISettings Members

        public new Setting this [string key]
        {
            get
            {
                if (ContainsKey (key))
                    return base [key];

                throw new SettingNotFoundException (key);
            }
            set
            {
                if (ContainsKey (key))
                    base [key] = value;
                else
                    Add (key, value);
            }
        }

        public Dictionary<string, object> ToKeyValueDictionary ()
        {
            Dictionary<string, object> results = new Dictionary<string, object> ();
            Enumerator enumerator = GetEnumerator ();
            while (enumerator.MoveNext ())
            {
                KeyValuePair<string, Setting> pair = enumerator.Current;
                results.Add (pair.Key, pair.Value.Value);
            }
            return results;
        }

        public ISettingsList ToReadOnly ()
        {
            return new ReadOnlySettings (this);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add (Setting setting)
        {
            if (setting == null)
                return;

            this [setting.Key] = setting;
        }

        public void AddSettings (ISettingsList settings)
        {
            if (settings == null)
                return;

            foreach (Setting setting in settings.Values)
            {
                Add (setting);
            }
        }

        #endregion
    }
}