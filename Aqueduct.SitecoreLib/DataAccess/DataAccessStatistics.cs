using Aqueduct.Diagnostics;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public static class DataAccessStatistics
    {
        public static void IncrementResolveEntity()
        {   
            Statistics.IncrementValue("ResolveEntity");
        }

        public static void IncrementResolveEntityFromCache()
        {
            Statistics.IncrementValue("ResolveEntityFromCache");
        }

        public static void IncrementFindMapByTemplatePath()
        {
            Statistics.IncrementValue("FindMapByTemplatePath");
        }

        public static void IncrementFindMapByType()
        {
            Statistics.IncrementValue("FindMapByType");
        }
    }
}