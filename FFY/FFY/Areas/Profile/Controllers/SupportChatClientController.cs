﻿using FFY.Web.Custom.Attributes;
using System.Web.Mvc;

namespace FFY.Web.Areas.Profile.Controllers
{
    [Localize]
    [Authorize]
    public class SupportChatClientController : Controller
    {
        // GET: Profile/SupportChatClient
        public ActionResult Index()
        {
            return this.View();
        }
    }
}