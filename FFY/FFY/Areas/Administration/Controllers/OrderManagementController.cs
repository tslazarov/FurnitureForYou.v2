using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    public class OrderManagementController : Controller
    {
        // GET: Administration/OrderManagement
        public ViewResult Index()
        {
            return this.View();
        }
    }
}