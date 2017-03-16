using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class UserManagementController : Controller
    {
        // GET: Administration/UserManagement
        public ViewResult Index()
        {
            return this.View();
        }
    }
}