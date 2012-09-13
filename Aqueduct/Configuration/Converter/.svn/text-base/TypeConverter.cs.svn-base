using System;
using System.Collections.Generic;
using System.Linq;
using Aqueduct.Extensions;

namespace Aqueduct.Configuration.Converter
{
    public class TypeConverter
    {
        private readonly Dictionary<string, Type> _typeAliases = new Dictionary<string, Type>();
        private readonly Dictionary<string, string> _genericTypeAliases = new Dictionary<string, string>();

        public TypeConverter()
        {
            AddDefaultAliases();
            AddGerenicAliases();
        }

        public TypeConverter(Dictionary<string, Type> typeAliases) :this()
        {
            
            foreach (KeyValuePair<string, Type> typeAlias in typeAliases)
            {
                _typeAliases[typeAlias.Key] = typeAlias.Value;
            }
        }

        private void AddDefaultAliases()
        {
            _typeAliases["int"] = typeof(Int32);
            _typeAliases["integer"] = typeof(Int32);
            _typeAliases["string"] = typeof(String);
            _typeAliases["double"] = typeof(Double);
            _typeAliases["float"] = typeof(Single);
            _typeAliases["single"] = typeof(Single);
            _typeAliases["byte"] = typeof(Byte);
            _typeAliases["bool"] = typeof(bool);
            _typeAliases["boolean"] = typeof(bool);
            _typeAliases["guid"] = typeof(Guid);
            _typeAliases["list"] = typeof(IList<string>);
        }

        private void AddGerenicAliases()
        {
            _genericTypeAliases["list"] = "System.Collections.Generic.IList`1";
            _genericTypeAliases["dictionary"] = "System.Collections.Generic.IDictionary`2";
        }

        public void RegisterAlias(string alias, Type type)
        {
            _typeAliases[alias] = type;
        }

        public Type ParseType(string typeName)
        {
            ConfigGuard.ArgumentNotNull(typeName, "typeName", "ParseType does not accept null");

            string parsedTypeName = ResolveGenericAliases(typeName);

            int startIndex = parsedTypeName.IndexOf('[');
            int endIndex = parsedTypeName.LastIndexOf(']');

            if (startIndex == -1)
            {
                // try to load the non-generic type (e.g. System.Int32)
                return GetNonGenericType(parsedTypeName);
            }
            else
            {
                // get the FullName of the non-generic type (e.g. System.Collections.Generic.List`1)
                string fullName = parsedTypeName.Substring(0, startIndex);
                if (parsedTypeName.Length - endIndex - 1 > 0)
                    fullName += parsedTypeName.Substring(endIndex + 1, parsedTypeName.Length - endIndex - 1);

                // parse the child type arguments for this generic type (recursive)
                List<Type> list = new List<Type>();
                string typeArguments = parsedTypeName.Substring(startIndex + 1, endIndex - startIndex - 1);
                foreach (string typeArgument in typeArguments.Split(",".ToCharArray()))
                {
                    list.Add(GetNonGenericType(typeArgument.Trim()));
                }

                return GetNonGenericType(fullName).MakeGenericType(list.ToArray());
            }
        }


        private string ResolveGenericAliases(string typeName)
        {
            if (typeName.Contains('['))
            {
                int bracketIndex = typeName.IndexOf('[');
                string aliasKey = typeName.Substring(0, bracketIndex).ToLower();
                if (_genericTypeAliases.ContainsKey(aliasKey))
                    return _genericTypeAliases[aliasKey] + typeName.Substring(bracketIndex);
            }

            return typeName;
        }

        private Type GetNonGenericType(string typeName)
        {
            Type result = null;

            if (!TryGetAliasedType(typeName, out result))
                return Type.GetType(typeName, false, true);

            return result;
        }

        private bool TryGetAliasedType(string type, out Type aliasType)
        {
            string key =
                _typeAliases.Keys.FirstOrDefault(k => k.Equals(type, StringComparison.CurrentCultureIgnoreCase));

            if (key.IsNotEmpty())
            {
                aliasType = _typeAliases[key];
                return true;
            }

            aliasType = null;
            return false;
        }
    }
}