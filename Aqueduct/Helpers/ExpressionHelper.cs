using System.Linq.Expressions;
using System;
using System.Reflection;

namespace Aqueduct.Helpers
{
    public static class ExpressionHelper
    {
        [Obsolete("Use the xml extensions in Aqueduct.Extensions instead")]
        public static string GetPropertyName<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            if (expression == null)
                return String.Empty;

            return GetPropertyInfo(expression).Name;
        }

        [Obsolete("Use the xml extensions in Aqueduct.Extensions instead")]
        public static string GetPropertyName<TResult>(this Expression<Func<TResult>> expression)
        {
            if (expression == null)
                return String.Empty;

            return GetPropertyInfo(expression).Name;
        }

        [Obsolete("Use the xml extensions in Aqueduct.Extensions instead")]
        private static PropertyInfo GetPropertyInfo(LambdaExpression expression)
        {
            var propertyInfo = ((MemberExpression)expression.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }
            return propertyInfo;
        }
    }
}
