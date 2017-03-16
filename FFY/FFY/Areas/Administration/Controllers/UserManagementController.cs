using FFY.Web.Custom.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Security(Roles = "Administrator, Moderator", RedirectUrl = "~/error/unauthorized")]
    public class UserManagementController : Controller
    {
        // GET: Administration/UserManagement
        public ViewResult Index()
        {
            return this.View();
        }
    }
}