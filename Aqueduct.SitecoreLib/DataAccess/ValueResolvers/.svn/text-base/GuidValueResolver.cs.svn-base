using System;

namespace Aqueduct.SitecoreLib.DataAccess.ValueResolvers
{
	public class GuidValueResolver : IValueResolver
	{
		public bool CanResolve(Type type)
		{
			return typeof (Guid) == type;
		}

		public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
		{
			return new Guid(rawValue);
		}

		public object ResolveItemFieldValue(object rawValue)
		{
			return rawValue;
		}
	}
}
