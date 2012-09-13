using System;

namespace Aqueduct.Configuration
{
    internal static class ConfigGuard
    {
        internal static void ThrowIfFalse (bool condition, string message)
        {
            throw new ConfigurationException (message);
        }

        internal static void ThrowInvalidOperation (Func<bool> condition, string message)
        {
            if (condition.Invoke ())
                throw new InvalidOperationException (message);
        }

        internal static void ArgumentNotNull(object arg, string paramName, string message)
        {
            if (arg == null)
                throw new ArgumentNullException(paramName, message);
        }
    }
}