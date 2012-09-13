using System;

namespace Aqueduct.Extensions
{
    /// <summary>
    /// Extension methods for guids
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Determine whether a Guid is empty, i.e. set to Guid.Empty
        /// </summary>
        public static bool IsEmpty (this Guid guid)
        {
            return guid.Equals (Guid.Empty);
        }

        public static string StripSpecialChars (this Guid guid)
        {
            string[] tokens = guid.ToString().Split(true, '{', '-', '}');
            return string.Join(String.Empty, tokens);

//             Or...
//             return guid.ToString("N"); ?
        }
    }
}
