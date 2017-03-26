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
    public class HttpRequestProvider : IHttpRequestProvider
    {
        public HttpFileCollectionBase GetRequestFiles(Controller controller)
        {
            return controller.Request.Files;
        }
    }
}
