using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    public class ProductManagementController : Controller
    {
        // GET: Administration/ProductManagement
        public ViewResult Index()
        {
            return this.View();
        }
    }
}