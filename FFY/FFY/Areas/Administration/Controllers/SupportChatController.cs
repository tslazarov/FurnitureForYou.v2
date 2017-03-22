using Bytes2you.Validation;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Models.SupportChat;
using FFY.Web.Custom.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Localize]
    [Security(Roles = "Administrator, Moderator", RedirectUrl = "~/error/unauthorized")]
    public class SupportChatController : Controller
    {
        private readonly IChatUsersService chatUsersService;

        public SupportChatController(IChatUsersService chatUsersService)
        {
            Guard.WhenArgument<IChatUsersService>(chatUsersService, "Chat users service cannot be null.")
                .IsNull()
                .Throw();

            this.chatUsersService = chatUsersService;
        }

        // GET: Administration/SupportChat
        public ActionResult Index(SupportChatViewModel model)
        {
            model.ConnectedUsers = this.chatUsersService.GetChatUsers();

            return this.View(model);
        }
    }
}