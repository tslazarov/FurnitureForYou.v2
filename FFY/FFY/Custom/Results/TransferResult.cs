using System;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Custom.Results
{
    public class TransferResult : ActionResult
    {
        public string Url { get; private set; }

        public TransferResult(string url)
        {
            this.Url = url;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var httpContext = HttpContext.Current;

            // MVC 3 running on IIS 7+
            httpContext.Server.TransferRequest(this.Url, true);
        }
    }
}