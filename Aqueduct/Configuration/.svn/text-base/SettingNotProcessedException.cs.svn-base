using System;

namespace Aqueduct.Configuration
{
    /// <summary>
    /// Thrown when a setting is tried to be Parsed when the property is not processed
    /// </summary>
    [Serializable]
    public class SettingNotProcessedException : Exception
    {
        private readonly Setting _setting;

        public Setting _setting1
        {
            get { return _setting; }
        }

        #region SettingNotProcessedException(string message)

        /// <summary>
        /// Constructs a new SettingNotProcessedException.
        /// </summary>
        /// <param name="message">The exception message</param>
        public SettingNotProcessedException (Setting setting)
            : base (String.Format ("Setting {0} is not processed", setting))
        {
            _setting = setting;
        }

        #endregion

        #region SettingNotProcessedException(string message, Exception innerException)

        /// <summary>
        /// Constructs a new SettingNotProcessedException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public SettingNotProcessedException (Setting setting, Exception innerException)
            : base (String.Format ("Setting {0} is not processed", setting), innerException)
        {
            _setting = setting;
        }

        #endregion

        // constructors...
    }
}