using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class ContactManagementController : Controller
    {
        // GET: Administration/ContactManagement
        public ViewResult Index()
        {
            return View();
        }
    }
}