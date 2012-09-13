using System;
using System.Collections.Generic;

namespace Aqueduct.Configuration.Validators
{
    public class OverridesValidator : ISettingValidator
    {
        private readonly List<string> _restrictedKeys = new List<string> ();

        /// <summary>
        /// Initializes a new instance of the NoDuplicatesValidator class.
        /// </summary>
        public OverridesValidator (IEnumerable<string> restrictedKeys)
        {
            _restrictedKeys.AddRange (restrictedKeys);
        }

        #region ISettingValidator Members

        public void Validate (Setting setting)
        {
            if (_restrictedKeys.Contains (setting.Key))
                throw new ValidationException (setting, String.Format ("Setting {0} cannot be overriden", setting.Key));
        }

        #endregion
    }
}