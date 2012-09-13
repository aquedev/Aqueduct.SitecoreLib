using System;
using System.Collections.Generic;
namespace Aqueduct.Helpers
{
    public static class EnumHelpers
    {
        

        public static IEnumerable <T> GetAll <T>()
            where T: struct
        {
            foreach (string name in Enum.GetNames (typeof(T)))
            {
#pragma warning disable 612,618
                yield return name.ToEnum<T> ();
#pragma warning restore 612,618
            }
        }
    }
}