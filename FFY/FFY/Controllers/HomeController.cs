using FFY.Web.Custom.Attributes;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    [Localize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}