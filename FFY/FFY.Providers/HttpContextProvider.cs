using FFY.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FFY.Providers
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContextBase GetHttpContext(Controller controller)
        {
            return controller.HttpContext;
        }

        public void InsertInCache(Controller controller, string key, object value)
        {
            this.GetHttpContext(controller).Cache.Insert(key, value);
        }
    }
}
