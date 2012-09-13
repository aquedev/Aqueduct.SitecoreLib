using System;
using System.Web.UI;

namespace Aqueduct.Extensions
{
    public static class ControlExtensions
    {
        public static decimal? ToDecimal (this ITextControl control)
        {
            return control.Text.ToDecimal ();
        }
        public static int? ToInt32 (this ITextControl control)
        {
            return control.Text.ToInt32();
        }
        public static DateTime? ToDateTime(this ITextControl control)
        {
            return control.Text.ToDateTime();
        }
        public static Guid? ToGuid(this ITextControl control)
        {
            return control.Text.ToGuid();
        }
    }
}
