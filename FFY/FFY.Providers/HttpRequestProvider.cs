using FFY.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFY.Providers
{
    public class HttpRequestProvider : IHttpRequestProvider
    {
        public HttpRequestBase Request
        {
            get
            {
                return Request;
            }
        }

        public HttpFileCollectionBase RequestFiles
        {
            get
            {
                return Request.Files;
            }
        }
    }
}
