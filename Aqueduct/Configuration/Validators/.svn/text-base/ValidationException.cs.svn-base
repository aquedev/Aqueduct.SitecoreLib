using System;

namespace Aqueduct.Configuration.Validators
{
    /// <summary>
    ///  The exception is used by the settings validators. 
    ///  It contains the setting that failed validation and the error message
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        public Setting Setting { get; set; }


        public ValidationException (Setting setting)
            : base (String.Format ("Setting with key {0} is invalid", setting.Key))
        {
            Setting = setting;
        }


        public ValidationException (Setting setting, string message) : base (message)
        {
            Setting = setting;
        }

    }
}