﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error/Unauthorized
        public ActionResult Unauthorized()
        {
            return this.View();
        }
    }
}