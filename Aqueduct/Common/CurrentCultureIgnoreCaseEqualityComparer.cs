using System;
using System.Collections.Generic;

namespace Aqueduct.Common
{

    public class CurrentCultureIgnoreCaseEqualityComparer : IEqualityComparer<string>
    {
        #region IEqualityComparer<string> Members

        public bool Equals (string x, string y)
        {
            return string.Equals (x, y, StringComparison.CurrentCultureIgnoreCase);
        }

        public int GetHashCode (string obj)
        {
            return obj.GetHashCode ();
        }

        #endregion
    }
}