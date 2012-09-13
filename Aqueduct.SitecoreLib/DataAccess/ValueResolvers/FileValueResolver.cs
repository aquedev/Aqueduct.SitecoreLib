using System;
using Aqueduct.SitecoreLib.DataAccess.Repositories;
using Aqueduct.SitecoreLib.Domain;
using System.Text.RegularExpressions;
using Aqueduct.Extensions;

namespace Aqueduct.SitecoreLib.DataAccess.ValueResolvers
{
    public class FileValueResolver : IValueResolver
    {
        private const string RegexPattern = @"<file.*?mediaid=""\{(?<mediaid>[\d\w-]+)\}"".*?/>";

        #region IValueResolver Members

        public bool CanResolve(Type type)
        {
            return type == typeof(File);
        }

        public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
            var result = new File();
            if (rawValue.IsNotEmpty())
            {
                Match imageMatch = Regex.Match(rawValue, RegexPattern);
                if (imageMatch.Success && imageMatch.Groups["mediaid"].Value.IsGuid())
                {
                    Guid imageId = new Guid(imageMatch.Groups["mediaid"].Value);
                    ReadOnlyRepository repository = new ReadOnlyRepository();
                    File image = repository.Get<File>(imageId);
                    result = image ?? new File();

                }
            }
            return result;
        }

        public object ResolveItemFieldValue(object rawValue)
        {
            var image = rawValue as File;

            if (image != null)
            {
                return string.Format("<file mediaid=\"{{{0}}}\" src=\"{1}\" />", image.Id, image.Url);
            }

            return string.Empty;
        }

        #endregion
    }
}
