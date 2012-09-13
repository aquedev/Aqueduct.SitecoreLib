using System;

namespace Aqueduct.Configuration
{
    public class Setting
    {
        protected bool _isProcessed;
        protected string _key;
        protected string _raw;
        protected Type _type;
        protected object _value;

        /// <summary>
        /// Initializes a new instance of the Setting class.
        /// </summary>
        public Setting ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Setting class.
        /// </summary>
        public Setting (string key, string raw, Type type)
        {
            Key = key;
            Raw = raw;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the Setting class.
        /// </summary>
        public Setting (string key, string raw, object value, Type type)
        {
            Key = key;
            Raw = raw;
            Value = value;
            Type = type;
        }

        public virtual string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        internal virtual String Raw
        {
            get { return _raw; }
            set { _raw = value; }
        }

        public virtual object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public virtual Type Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public virtual bool IsProcessed
        {
            get { return _isProcessed; }
            set { _isProcessed = value; }
        }

        public static Setting Copy (Setting setting)
        {
            return new Setting (setting.Key, setting.Raw, setting.Value, setting.Type);
        }

        /// <summary>
        /// A processed setting will be exposed by the confighandler as is and no further processing ( convern
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="settingType"></param>
        /// <returns></returns>
        public static Setting CreateProcessedSetting(string key, object value)
        {
            var setting = new Setting(key, value.ToString(), value, value.GetType());
            setting.IsProcessed = true;
            return setting;
        }

        public T GetValue<T> ()
        {
            return GetValue (default(T));
        }

        public T GetValue<T> (T defaultValue)
        {
            if (Value == null)
                return defaultValue;
            return (T) Value;
        }

        public Setting ToReadOnly ()
        {
            return new ReadOnlySetting (this);
        }

        public override string ToString ()
        {
            return String.Format ("Key: {0}, value: {1}, type: {2}, raw: {3}, isProcessed: {4}", Key, Value,
                                  Type.FullName, Raw, IsProcessed);
        }
    }

    public class Setting<T> : Setting
    {
        /// <summary>
        /// Initializes a new instance of the Setting class.
        /// </summary>
        public Setting (string key, string raw, object value, Type type) : base (key, raw, value, type)
        {
        }

        public Setting (string key, string raw, Type type)
            : base (key, raw, type)
        {
        }

        public new T Value
        {
            get { return (T) base.Value; }
        }
    }
}