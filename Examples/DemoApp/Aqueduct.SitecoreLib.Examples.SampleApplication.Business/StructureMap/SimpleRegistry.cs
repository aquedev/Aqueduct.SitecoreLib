using Aqueduct.SitecoreLib.DataAccess;
using Aqueduct.SitecoreLib.DataAccess.Caches;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.DataAccess.StructureMap;
using StructureMap.Configuration.DSL;

namespace Aqueduct.SitecoreLib.Examples.SampleApplication.Business.StructureMap
{
    public class SimpleRegistry : Registry
    {
        public SimpleRegistry(string assemblyPrefix)
        {
            For<IReadOnlyRepository>().Use(new ReadOnlyRepository("web"));
            For<IContextLevelCache>().Use<HybridContextLevelCache>();

            Scan(scan =>
                     {
                         scan.AssembliesFromApplicationBaseDirectory(
                             a => a.GetName().Name.StartsWith("Aqueduct") || a.GetName().Name.StartsWith(assemblyPrefix));
                         scan.WithDefaultConventions();

                         scan.Convention<MapConventionScanner>();
                         scan.Convention<MapTemplatePathScanner>();
                     });
        }
    }
}