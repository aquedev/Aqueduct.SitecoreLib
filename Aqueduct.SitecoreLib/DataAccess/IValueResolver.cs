using System;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public interface IValueResolver
    {
        bool CanResolve(Type type);

        object ResolveEntityPropertyValue(string rawValue, Type propertyType);
        object ResolveItemFieldValue(object rawValue);
    }
}
