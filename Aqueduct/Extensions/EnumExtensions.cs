using System;
using System.ComponentModel;

namespace Aqueduct.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum e)
        {
            var da = (DescriptionAttribute[])(e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false));
            return da.Length > 0 ? da[0].Description : e.ToString();
        }
    }
}
