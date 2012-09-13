using System.Web.UI;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.ContentEditor;

namespace Aqueduct.SitecoreLib.CustomFields
{
    public class TextDroplist : LookupEx
    {
        protected override void DoRender(HtmlTextWriter output)
        {
            string[] items = Source.Split('|');
            output.Write("<select" + GetControlAttributes() + ">");
            output.Write("<option value=\"\"></option>");
            bool found = false;
            foreach (string item in items)
            {
                bool isSelected = IsSelected(item);
                if (isSelected)
                {
                    found = true;
                }
                output.Write("<option value=\"" + item + "\"" + (isSelected ? " selected=\"selected\"" : string.Empty) +
                             ">" + item + "</option>");
            }
            bool isNotInList = !string.IsNullOrEmpty(Value) && !found;
            if (isNotInList)
            {
                output.Write("<optgroup label=\"" + Translate.Text("Value not in the selection list.") + "\">");
                output.Write("<option value=\"" + Value + "\" selected=\"selected\">" + Value + "</option>");
                output.Write("</optgroup>");
            }
            output.Write("</select>");
            if (isNotInList)
            {
                output.Write("<div style=\"color:#999999;padding:2px 0px 0px 0px\">{0}</div>",
                             Translate.Text("The field contains a value that is not in the selection list."));
            }
        }

        protected virtual bool IsSelected(string text)
        {
            return Value == text;
        }
    }
}
