using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Aqueduct.Configuration
{
    public class ReadOnlySettings : ISettingsList
    {
        private readonly SettingsList _internalStorage = new SettingsList ();

        /// <summary>
        /// Initializes a new instance of the ReadOnlySettingsList class.
        /// </summary>
        public ReadOnlySettings (IDictionary<string, Setting> settings)
        {
            foreach (Setting setting in settings.Values)
            {
                Setting set = setting.ToReadOnly ();
                _internalStorage.Add (set.Key, set);
            }
        }

        #region ISettings Members

        public Setting this [string key]
        {
            get { return _internalStorage [key]; }
            set { throw new NotSupportedException ("Settings cannot be added to a readonly list"); }
        }

        public bool ContainsKey (string key)
        {
            return _internalStorage.ContainsKey (key);
        }

        public Dictionary<string, object> ToKeyValueDictionary ()
        {
            Dictionary<string, object> results = new Dictionary<string, object> ();
            foreach (Setting setting in Values)
            {
                results.Add (setting.Key, setting.Value);
            }
            return results;
        }

        public ISettingsList ToReadOnly ()
        {
            return this;
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public void Add (Setting setting)
        {
            throw new ReadOnlyException ("Cannot add items from the collection because it's readonly");
        }

        public void AddSettings (ISettingsList settings)
        {
            throw new ReadOnlyException ("Cannot add items from the collection because it's readonly");
        }

        public void Add (string key, Setting value)
        {
            throw new ReadOnlyException ("Cannot add settings because the ISettings dictionary is readonly");
        }

        public ICollection<string> Keys
        {
            get { return _internalStorage.Keys; }
        }

        public bool Remove (string key)
        {
            throw new ReadOnlyException ("Cannot remove Settings because the ISettings dictionary is readonly");
        }

        public bool TryGetValue (string key, out Setting value)
        {
            return _internalStorage.TryGetValue (key, out value);
        }

        public ICollection<Setting> Values
        {
            get { return _internalStorage.Values; }
        }

        public void Add (KeyValuePair<string, Setting> item)
        {
            throw new ReadOnlyException ("Cannot add settings because the ISettings dictionary is readonly");
        }

        public void Clear ()
        {
            throw new ReadOnlyException ("Cannot clear the settings because the ISettings dictionary is readonly");
        }

        public bool Contains (KeyValuePair<string, Setting> item)
        {
            return _internalStorage.Contains (item);
        }

        public void CopyTo (KeyValuePair<string, Setting>[] array, int arrayIndex)
        {
            var keyValueCollection = _internalStorage as ICollection<KeyValuePair<string, Setting>>;
            keyValueCollection.CopyTo (array, arrayIndex);
        }

        public int Count
        {
            get { return _internalStorage.Count; }
        }

        public bool Remove (KeyValuePair<string, Setting> item)
        {
            throw new ReadOnlyException ("Cannot remove items from the collection because it's readonly");
        }

        public IEnumerator<KeyValuePair<string, Setting>> GetEnumerator ()
        {
            return _internalStorage.GetEnumerator ();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return _internalStorage.GetEnumerator ();
        }

        #endregion
    }
}