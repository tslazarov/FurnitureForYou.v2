using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Profile.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        // GET: Profile/ShoppingCart
        public ViewResult Index()
        {
            return this.View();
        }
    }
}