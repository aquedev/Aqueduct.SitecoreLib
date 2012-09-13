using Aqueduct.SitecoreLib.Domain;

namespace Aqueduct.SitecoreLib.DataAccess.Maps
{
    public class ImageMap : Map<Image>
    {
        public ImageMap()
        {
            MapProperty(image => image.Alt);
            MapProperty(image => image.Height);
            MapProperty(image => image.Width);
            MapProperty(image => image.Dimensions);
            MapProperty(image => image.Description);
            MapProperty(image => image.Title);
        }

        public override string TemplatePath
        {
            get { return "System/Media/Unversioned/Image"; }
        }
    }
}
