using FFY.Web.Custom.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Custom.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SecurityAttribute : AuthorizeAttribute
    {
        private string redirectUrl;

        public string RedirectUrl
        {
            get
            {
                return this.redirectUrl;
            }
            set
            {
                this.redirectUrl = value;
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                this.SetCachePolicy(filterContext);
            }
            else if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (this.RedirectUrl != null)
                {
                    filterContext.Result = new TransferResult(RedirectUrl);
                }
                else
                {
                    HandleUnauthorizedRequest(filterContext);
                }
            }
            else
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        private void SetCachePolicy(AuthorizationContext filterContext)
        {
            // ** IMPORTANT **
            // Since we're performing authorization at the action level, the authorization code runs
            // after the output caching module. In the worst case this could allow an authorized user
            // to cause the page to be cached, then an unauthorized user would later be served the
            // cached page. We work around this by telling proxies not to cache the sensitive page,
            // then we hook our custom authorization code into the caching mechanism so that we have
            // the final say on whether a page should be served from the cache.
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.AddValidationCallback(CacheValidateHandler, null);
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }
    }
}