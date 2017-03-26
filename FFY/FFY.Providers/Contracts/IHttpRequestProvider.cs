using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFY.Providers.Contracts
{
    public interface IHttpRequestProvider
    {
        HttpRequestBase Request { get; }

        HttpFileCollectionBase RequestFiles { get; }
    }
}
