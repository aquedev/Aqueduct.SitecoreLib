using System;

namespace Aqueduct.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class InvalidTypeException : Exception
    {
        private readonly string _Type;

        public string Type
        {
            get { return _Type; }
        }

        #region InvalidTypeException(string message)

        /// <summary>
        /// Constructs a new InvalidTypeException.
        /// </summary>
        /// <param name="message">The exception message</param>
        public InvalidTypeException (string type) : base ("Invalid type " + type)
        {
            _Type = type;
        }

        #endregion

        #region InvalidTypeException(string message, Exception innerException)

        /// <summary>
        /// Constructs a new InvalidTypeException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public InvalidTypeException (string type, Exception innerException)
            : base ("Invalid type " + type, innerException)
        {
            _Type = type;
        }

        #endregion
    }
}