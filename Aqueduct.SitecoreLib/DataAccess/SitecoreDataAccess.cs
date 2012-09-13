using Aqueduct.DataAccess;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public static class SitecoreDataAccess
    {
        internal static ISitecoreDataAccessSettings Settings { get; private set; }

        public static void Initialize(ISitecoreDataAccessSettings settings)
        {
            Settings = settings;
        }
    }
}
