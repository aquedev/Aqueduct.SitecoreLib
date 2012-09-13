using System;

namespace Aqueduct.Extensions
{
    public static class ComparableExtensions
    {
        public static bool Between<T>(this T value, T low, T high)
            where T : IComparable<T>
        {
            return (value.CompareTo(low) > -1 && value.CompareTo(high) < 1);
        }
        public static T RestrictToRange<T>(this T value, T low, T high)
            where T : IComparable<T>
        {
            T result = value;

            if (result.CompareTo(low) < 0)
                result = low;
            if (result.CompareTo(high) > 0)
                result = high;
            
            return result;
        }
    }
}
