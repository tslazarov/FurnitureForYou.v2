using FFY.Providers.Contracts;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;

namespace FFY.Providers
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContext CurrentHttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public IIdentity CurrentIdentity
        {
            get
            {
                return HttpContext.Current.User.Identity;
            }
        }

        public Cache CurrentCache
        {
            get
            {
                return HttpContext.Current.Cache;
            }
        }

        public IOwinContext CurrentOwinContext
        {
            get
            {
                return HttpContext.Current.GetOwinContext();
            }
        }

        public TManager GetCurrentUserManager<TManager>()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<TManager>();
        }
    }
}
