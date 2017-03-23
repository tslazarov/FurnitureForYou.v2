using Bytes2you.Validation;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Models.UserManagement;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Custom.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Localize]
    [Security(Roles = "Administrator", RedirectUrl = "~/error/unauthorized")]
    public class UserManagementController : Controller
    {
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IUsersService usersService;

        public UserManagementController(IAuthenticationProvider authenticationProvider,
            IUsersService usersService)
        {
            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
               .IsNull()
               .Throw();

            Guard.WhenArgument<IUsersService>(usersService, "Users service cannot be null.")
                .IsNull()
                .Throw();

            this.authenticationProvider = authenticationProvider;
            this.usersService = usersService;
        }

        // GET: Administration/UserManagement
        public ViewResult Index()
        {
            return this.View();
        }

        // GET: Administration/Users/Id
        public ViewResult UserProfile(UserViewModel model, ProfileViewModel profileModel, string id)
        {
            model.ProfileModel = profileModel;
            model.ProfileModel.User = this.usersService.GetUserById(id);

            model.Role = this.authenticationProvider.GetUserRoles(id).First();
            model.UserId = id;

            return this.View(model);
        }

        // POST: Administration/UserManagement/UpdateStatus
        public ActionResult UpdateStatus(UserViewModel model)
        {
            var user = this.usersService.GetUserById(model.UserId);

            this.authenticationProvider.ChangeUserRole(user.Id, model.Role);

            this.authenticationProvider.UpdateSecurityStamp(user.Id);

            return this.RedirectToAction("UserProfile", new { id = model.UserId });
        }
    }
}