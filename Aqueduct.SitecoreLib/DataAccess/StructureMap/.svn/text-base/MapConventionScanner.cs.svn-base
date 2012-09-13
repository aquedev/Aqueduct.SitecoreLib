using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Aqueduct.SitecoreLib.DataAccess.StructureMap
{
	public class MapConventionScanner : IRegistrationConvention
	{
		private readonly Type m_mapType = typeof (Map<>);

		#region ITypeScanner Members

        public void Process(Type type, Registry registry)
        {
            if (!type.IsConcrete()) return;

            var baseType = type.BaseType;
            if (baseType != null && baseType.IsGenericType && baseType.GetGenericTypeDefinition() == m_mapType)
            {
                registry.AddType(baseType, type);
            }
        }


		#endregion
	}
}