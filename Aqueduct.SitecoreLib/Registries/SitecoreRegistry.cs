using Aqueduct.DataAccess;
using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.DataAccess.Caches;
using Aqueduct.SitecoreLib.DataAccess.StructureMap;
using StructureMap.Configuration.DSL;

namespace Aqueduct.SitecoreLib.Registries
{
    public class SitecoreRegistry : Registry
    {
        public SitecoreRegistry()
        {
            For<IContextLevelCache>().Use(cache => new HybridContextLevelCache());

            Scan(scanner =>
            {
                scanner.AssembliesFromApplicationBaseDirectory();
                scanner.AssemblyContainingType(typeof(IValueResolver));
                scanner.AddAllTypesOf(typeof(ISitecoreDataAccessSettings));
                scanner.LookForRegistries();
                scanner.Convention<MapConventionScanner>();
                scanner.Convention<MapTemplatePathScanner>();
            });
        }
    }
}
