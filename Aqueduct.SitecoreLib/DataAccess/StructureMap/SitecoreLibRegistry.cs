using StructureMap.Configuration.DSL;

namespace Aqueduct.SitecoreLib.DataAccess.StructureMap
{
	public class SitecoreLibRegistry : Registry
	{
		public SitecoreLibRegistry(string dataAccessAssemblyName)
		{
			Scan(x =>
			     	{
                        x.Assembly(dataAccessAssemblyName);
                        x.Convention<MapConventionScanner>();
						x.Convention<MapTemplatePathScanner>();
			     	});
		}
	}
}