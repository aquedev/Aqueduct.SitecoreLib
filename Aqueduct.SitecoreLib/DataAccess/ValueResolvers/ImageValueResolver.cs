using System;
using System.Text.RegularExpressions;
using Aqueduct.Extensions;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.Domain;

namespace Aqueduct.SitecoreLib.DataAccess.ValueResolvers
{
    public class ImageValueResolver : IValueResolver
    {
        private const string RegexPattern = @"<image.*?mediaid=""\{(?<imageid>[\d\w-]+)\}"".*?/>";

        #region IValueResolver Members

        public bool CanResolve(Type type)
        {
            return type == typeof(Image);
        }

        public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
            if (rawValue.IsNotEmpty())
            {
                Match imageMatch = Regex.Match(rawValue, RegexPattern);
                if (imageMatch.Success && imageMatch.Groups["imageid"].Value.IsGuid())
                {
                    Guid imageId = new Guid(imageMatch.Groups["imageid"].Value);
                    ReadOnlyRepository repository = new ReadOnlyRepository();
                    return repository.Get<Image>(imageId);
                }
            }

            return null;
        }

        public object ResolveItemFieldValue(object rawValue)
        {
            var image = rawValue as Image;

            if (image != null)
            {
                return string.Format("<image mediaid=\"{{{0}}}\" mediapath=\"\" src=\"{1}\" />", image.Id, image.Url);
            }

            return string.Empty;
        }

        #endregion
    }

}
