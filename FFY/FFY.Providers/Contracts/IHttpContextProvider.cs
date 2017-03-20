using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace FFY.Providers.Contracts
{
    public interface IHttpContextProvider
    {
        HttpContextBase GetHttpContext(Controller controller);

        void InsertInCache(Controller controller, string key, object value);
    }
}
