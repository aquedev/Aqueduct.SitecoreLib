using System;

namespace Aqueduct.Configuration
{
    /// <summary>
    /// Exception is thrown if a setting of the wrong type is passed to a settingparser that accepts a different type
    /// </summary>
    [Serializable]
    public class SettingParseInvalidTypeException : Exception
    {
        public Type _parsedType { get; protected set; }
        public Setting _setting { get; protected set; }

        #region InvalidSettingTypeException()

        /// <summary>
        /// Constructs a new InvalidSettingTypeException.
        /// </summary>
        public SettingParseInvalidTypeException (Setting setting, Type parsedType)
            : base (
                String.Format ("Expecting setting type: {0}, doesn't match setting: {1}", parsedType.FullName, setting))
        {
            _setting = setting;
            _parsedType = parsedType;
        }

        #endregion

        #region InvalidSettingTypeException(string message, Exception innerException)

        /// <summary>
        /// Constructs a new InvalidSettingTypeException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public SettingParseInvalidTypeException (Setting setting, Type parsedType, Exception innerException)
            : base (
                String.Format ("Expecting setting type: {0}, doesn't match setting: {1}", parsedType.FullName, setting),
                innerException)
        {
            _setting = setting;
            _parsedType = parsedType;
        }

        #endregion

        // constructors...
    }
}