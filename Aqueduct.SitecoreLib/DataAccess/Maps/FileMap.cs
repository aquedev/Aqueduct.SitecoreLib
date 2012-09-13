using Aqueduct.SitecoreLib.Domain;

namespace Aqueduct.SitecoreLib.DataAccess.Maps
{
    public class FileMap : Map<File>
    {
        public override string TemplatePath
        {
            get { return ""; }
        }

        public override bool SkipTemplateChecking
        {
            get { return true; }
        }
    }
}
