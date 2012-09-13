using System;

namespace Aqueduct.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SettingNotFoundException : Exception
    {
        #region SettingNotFoundException()

        /// <summary>
        /// Constructs a new SettingNotFoundException.
        /// </summary>
        public SettingNotFoundException (string key)
            : base (String.Format ("Setting with key {0} not found in the config", key))
        {
        }

        #endregion

        #region SettingNotFoundException(string message, Exception innerException)

        /// <summary>
        /// Constructs a new SettingNotFoundException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public SettingNotFoundException (string key, Exception innerException)
            : base (String.Format ("Setting with key {0} not found in the config", key), innerException)
        {
        }

        #endregion

        // constructors...
    }
}