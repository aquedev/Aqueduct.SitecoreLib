using System;

namespace Aqueduct.SitecoreLib.Domain
{
    [Serializable]
    public class Image : DomainEntity
    {
        public string Alt { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Dimensions { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(Url); }
        }

        public static bool IsNullOrEmpty(Image image)
        {
            if (image == null)
                return true;
            return image.IsEmpty;
        }

        public new string Url
        {
            get
            {
                var url = base.Url;

                return (!string.IsNullOrEmpty(url) && url.StartsWith("~")) ? string.Format("/{0}", url) : url;
            }
            set { base.Url = value; }
        }
    }
}
