using System.Reflection;
using Aqueduct.SitecoreLib.DataAccess.SitecoreResolvers;

namespace Aqueduct.SitecoreLib.DataAccess
{
    public class MapEntry
    {
        public MapEntry(PropertyInfo mappedProperty)
        {
            MappedProperty = mappedProperty;
            MappedTo = MappedProperty.Name;
			DontSetIfEmptyProperty = false;
        	HasResolver = false;
        }

        public PropertyInfo MappedProperty { get; private set; }
        public string MappedTo { get; private set; }
        public object DefaultValue { get; set; }
		internal ISitecoreResolver Resolver { get; private set; }
		internal bool HasResolver { get; private set; }

        public bool HasDefaultValue
        {
            get { return DefaultValue != null; }
        }

		public MapEntry MapTo(string mapToName)
        {
            MappedTo = mapToName;
            return this;
        }

    	public void SetResolver(ISitecoreResolver resolver)
		{
			Resolver = resolver;
			HasResolver = true;
		}

		public MapEntry DefaultTo(object defaultValue)
        {
            DefaultValue = defaultValue;
            return this;
        }

		public bool DontSetIfEmptyProperty { get; private set; }
		public MapEntry DontSetIfEmpty()
    	{
			DontSetIfEmptyProperty = true;
    		return this;
    	}

    }
}