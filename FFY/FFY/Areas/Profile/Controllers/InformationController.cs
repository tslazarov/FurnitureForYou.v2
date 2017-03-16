using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Profile.Controllers
{
    [Authorize]
    public class InformationController : Controller
    {
        // GET: Profile/Information
        public ViewResult Index()
        {
            return View();
        }
    }
}