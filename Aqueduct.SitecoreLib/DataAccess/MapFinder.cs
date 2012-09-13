using StructureMap;
using Aqueduct.Domain;
using System;

namespace Aqueduct.SitecoreLib.DataAccess
{
	public static class MapFinder
	{
		public static Map<TDomainEntity> FindMap<TDomainEntity>() 
            where TDomainEntity : ISitecoreDomainEntity
		{
            DataAccessStatistics.IncrementFindMapByType();
			return ObjectFactory.TryGetInstance<Map<TDomainEntity>>();
		}

        public static IMap FindMap(Type domainEntityType)
        {
            DataAccessStatistics.IncrementFindMapByType();
            return ObjectFactory.TryGetInstance(typeof(Map<>).MakeGenericType(domainEntityType)) as IMap;
        }

		public static IMap FindMap(string templatePath)
		{
            DataAccessStatistics.IncrementFindMapByTemplatePath();
			return ObjectFactory.TryGetInstance<IMap>(templatePath.ToLower());
		}
        
	}
}