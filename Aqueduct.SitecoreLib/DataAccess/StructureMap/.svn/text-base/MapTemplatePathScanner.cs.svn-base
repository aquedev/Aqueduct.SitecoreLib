using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Aqueduct.SitecoreLib.DataAccess.StructureMap
{
	public class MapTemplatePathScanner : IRegistrationConvention
	{
		private readonly Type m_mapType = typeof (Map<>);

	    #region Implementation of IRegistrationConvention

	    public void Process(Type type, Registry registry)
	    {
            // ignore interfaces and abstract classes
            if (type.IsConcrete() == false)
                return;

            Type baseType = type.BaseType;

            // IF type has basetype AND basetype is generic AND basetype is Map<> THEN
            if (baseType != null && baseType.IsGenericType && baseType.GetGenericTypeDefinition() == m_mapType)
            {
                var instance = Activator.CreateInstance(type);
                string instanceName = instance.GetType().GetProperty("TemplatePath").GetValue(instance, null).ToString();
                registry.AddType(typeof(IMap), type, instanceName.ToLower());
            }
	    }

	    #endregion
	}
}