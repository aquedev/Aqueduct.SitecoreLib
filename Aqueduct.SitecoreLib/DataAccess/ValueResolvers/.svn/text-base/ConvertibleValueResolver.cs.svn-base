using System;

namespace Aqueduct.SitecoreLib.DataAccess.ValueResolvers
{
    public class ConvertibleValueResolver : IValueResolver
    {
        #region IValueResolver Members

        public virtual bool CanResolve (Type type)
        {
            // Can resolve any type which implements IConvertible, except DateTime, 
            // Boolean or Enum, which are more explicitly handled by other value 
            // resolvers
            if (type.Equals (typeof (DateTime)) || type.Equals (typeof (Boolean)) || type.IsEnum)
            {
                return false;
            }

            return typeof (IConvertible).IsAssignableFrom (type);
        }

        public virtual object ResolveEntityPropertyValue (string rawValue, Type propertyType)
        {
            return Convert.ChangeType (rawValue, propertyType);
        }

        public virtual object ResolveItemFieldValue (object rawValue)
        {
            return rawValue;
        }

        #endregion
    }

}