using System;

namespace Aqueduct.Extensions
{
    public static class CharExtensions
    {
        public static char ToLower(this char source)
        {
            return Char.ToLowerInvariant (source);
        }

        public static char ToUpper(this char source)
        {
            return Char.ToUpperInvariant (source);
        }
    }
}