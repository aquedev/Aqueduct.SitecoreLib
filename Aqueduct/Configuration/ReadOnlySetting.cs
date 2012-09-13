using System;
using System.Data;

namespace Aqueduct.Configuration
{
    public class ReadOnlySetting : Setting
    {
        private const string ReadOnlyExceptionMessage = "The setting is readonly. Cannot change the value of '{0}'";

        /// <summary>
        /// Initializes a new instance of the ReadOnlySetting class.
        /// </summary>
        internal ReadOnlySetting (Setting setting)
        {
            _isProcessed = setting.IsProcessed;
            _key = setting.Key;
            _raw = setting.Raw;
            _type = setting.Type;
            _value = setting.Value;
        }

        internal override string Raw
        {
            get { return base.Raw; }
            set { throw new ReadOnlyException (string.Format (ReadOnlyExceptionMessage, "Raw")); }
        }

        public override bool IsProcessed
        {
            get { return base.IsProcessed; }
            set { throw new ReadOnlyException (string.Format (ReadOnlyExceptionMessage, "IsProcessed")); }
        }

        public override string Key
        {
            get { return base.Key; }
            set { throw new ReadOnlyException (string.Format (ReadOnlyExceptionMessage, "Key")); }
        }

        public override Type Type
        {
            get { return base.Type; }
            set { throw new ReadOnlyException (string.Format (ReadOnlyExceptionMessage, "Type")); }
        }

        public override object Value
        {
            get { return base.Value; }
            set { throw new ReadOnlyException (string.Format (ReadOnlyExceptionMessage, "Value")); }
        }
    }
}