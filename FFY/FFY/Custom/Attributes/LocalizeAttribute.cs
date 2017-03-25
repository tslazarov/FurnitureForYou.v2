using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace FFY.Web.Custom.Attributes
{
    public class LocalizeAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string language = (string)filterContext.RouteData.Values["language"] ?? "en";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);

        }
    }
}