using System.Collections.Generic;
using System.Web;
using Aqueduct.Diagnostics;

namespace Aqueduct.Web.Diagnostics
{
    public class HttpContextStatisticsStore : BaseStatisticsStore
    {
        private const string StoreKey = "HttpContextStatisticsStore";

        private HttpContext CurrentContext
        {
            get { return HttpContext.Current; }
        }
        
        public override bool IsActive()
        {
            return CurrentContext != null;
        }

        public override void Clear()
        {
            // do nothing the context will be cleared after this request anyway
        }

        protected override IDictionary<string, long> Dictionary
        {
            get
            {
                if (HttpContext.Current.Items[StoreKey] == null)
                    HttpContext.Current.Items[StoreKey] = new Dictionary<string, long>();
                return HttpContext.Current.Items[StoreKey] as IDictionary<string, long>;
            }
        }
    }
}
