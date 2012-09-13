using System;

namespace Aqueduct.Helpers
{
    public static class EnumExtensions
    {
        [Obsolete("Use the string extensions in Aqueduct.Extensions instead")]
        public static T ParseAsEnum<T>(this string value, T defaultValue)
            where T : struct
        {
            try
            {
                return value.ToEnum<T>();
            }
            catch (ArgumentException)
            {
                return defaultValue;
            }
        }


        [Obsolete("Use the string extensions in Aqueduct.Extensions instead")]
        public static T ToEnum<T>(this string value)
            where T : struct
        {
           
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
