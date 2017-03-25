using Microsoft.Owin;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;

namespace FFY.Providers.Contracts
{
    public interface IHttpContextProvider
    {
        HttpContext CurrentHttpContext { get; }

        IIdentity CurrentIdentity { get; }

        IOwinContext CurrentOwinContext { get; }

        Cache CurrentCache { get; }

        TManager GetCurrentUserManager<TManager>();

    }
}
