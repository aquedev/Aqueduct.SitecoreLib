using System;
using System.Collections.Specialized;
using System.Web;

namespace Aqueduct.Web
{
    public abstract class RequestParams
    {
        private ParamsType m_paramType = ParamsType.Querystring;

        public RequestParams()
        {

        }

        public RequestParams(ParamsType paramType)
        {
            m_paramType = paramType;
            
        }

        public enum ParamsType
        {
            Querystring,
            AllFormParams
        }

        protected NameValueCollection Parameters
        {
            get
            {
                switch (m_paramType)
                {
                    default:
                    case ParamsType.Querystring:
                        return Context.Request.QueryString;
                        case ParamsType.AllFormParams:
                        	return Context.Request.Params;
                }
            }
        }

        private HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        #region Helper methods

        protected bool HasValue(string key)
        {
            return !String.IsNullOrEmpty(GetParameter(key, string.Empty));
        }

        protected T GetParameter<T>(string key, T defaultValue)
            where T : IConvertible
        {
            string setting = GetParameter(key, defaultValue.ToString());

            try
            {
                return (T)Convert.ChangeType(setting, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        protected T GetEnumParameter<T>(string key, T defaultValue)
            where T : struct
        {
            string setting = GetParameter(key, defaultValue.ToString());

            try
            {
                return (T)Enum.Parse(typeof(T), setting, true);
            }
            catch
            {
                return defaultValue;
            }
        }

        protected string GetParameter(string key, string defaultValue)
        {
            string value = Parameters[key];
            return String.IsNullOrEmpty(value) ? defaultValue : value;
        }

        

        protected Guid GetParameter(string key, Guid defaultValue)
        {
            try
            {
                return new Guid(Parameters[key]);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        protected T GetMandatoryParam<T>(string key)
            where T : IConvertible, IFormattable
        {
            string setting = GetMandatoryParameter(key);
            return (T)Convert.ChangeType(setting, typeof(T));
        }

        protected string GetMandatoryParameter(string key)
        {
            string result = GetParameter(key, null);

            if (result == null)
            {
                throw new Exception(String.Format("No value found for \"{0}\", this value is mandatory", key));
            }
            return result;
        }

        protected bool GetBoolParameter(string key, bool defaultValue)
        {
            string value = GetParameter(key, null);
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            switch (value.ToLower())
            {
                case "1":
                case "yes":
                case "on":
                case "true":
                    return true;
                default:
                    return false;
            }
        }

        #endregion

    }
}
