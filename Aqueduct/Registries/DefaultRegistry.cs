using StructureMap.Configuration.DSL;

namespace Aqueduct.Registries
{
    /// <summary>
    /// The Default StructureMap registry for AqueductLib
    /// </summary>
    public class DefaultRegistry : Registry
    {
        /// <summary>
        /// The Default registry
        /// </summary>
        public DefaultRegistry()
        {
            Scan(scanner =>
	                {
	                    scanner.AssembliesFromApplicationBaseDirectory(assembly => assembly.GetName().Name.Contains("Presentation"));
	                    scanner.AssembliesFromApplicationBaseDirectory(assembly => assembly.GetName().Name.Contains("Business"));
	                    scanner.AssembliesFromApplicationBaseDirectory(assembly => assembly.GetName().Name.Contains("DataAccess"));
	                    scanner.WithDefaultConventions();
	                });
        }
    }
}
