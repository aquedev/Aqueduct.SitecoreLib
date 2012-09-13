using System;
using System.Runtime.Serialization;

namespace Aqueduct.Configuration
{
    [Serializable]
    public class ConfigurationException : Exception
    {
        #region ConfigurationLoaderException(string message)

        /// <summary>
        /// Constructs a new ConfigurationLoaderException.
        /// </summary>
        /// <param name="message">The exception message</param>
        public ConfigurationException (string message) : base (message)
        {
        }

        #endregion

        #region ConfigurationLoaderException(string message, Exception innerException)

        /// <summary>
        /// Constructs a new ConfigurationLoaderException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public ConfigurationException (string message, Exception innerException) : base (message, innerException)
        {
        }

        #endregion

        #region ConfigurationLoaderException(SerializationInfo info, StreamingContext context)

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        protected ConfigurationException (SerializationInfo info, StreamingContext context) : base (info, context)
        {
        }

        #endregion

        // constructors...
    }
}