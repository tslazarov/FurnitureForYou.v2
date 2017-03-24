using Bytes2you.Validation;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Models;
using FFY.Web.Areas.Administration.Models.UserManagement;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Custom.Attributes;
using FFY.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Localize]
    [Security(Roles = "Administrator", RedirectUrl = "~/en/error/unauthorized")]
    public class UserManagementController : Controller
    {
        private const int UsersPerPage = 10;

        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IMapperProvider mapper;
        private readonly IUsersService usersService;

        public UserManagementController(IAuthenticationProvider authenticationProvider,
            IMapperProvider mapper,
            IUsersService usersService)
        {
            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
               .IsNull()
               .Throw();

            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
               .IsNull()
               .Throw();

            Guard.WhenArgument<IUsersService>(usersService, "Users service cannot be null.")
                .IsNull()
                .Throw();

            this.authenticationProvider = authenticationProvider;
            this.mapper = mapper;
            this.usersService = usersService;
        }

        // GET: Administration/UserManagement
        public ViewResult Index(UsersViewModel model)
        {
            return this.View(model);
        }


        // GET: Administration/SearchUsers
        public PartialViewResult SearchUsers(SearchModel searchModel, UsersViewModel usersModel, int? page)
        {
            int actualPage = page ?? 1;

            var result = this.usersService.SearchUsers(searchModel.SearchWord, searchModel.SortBy, actualPage, UsersPerPage);
            var count = this.usersService.GetUsersCount(searchModel.SearchWord);

            usersModel.SearchModel = searchModel;
            usersModel.UsersCount = count;
            usersModel.Pages = (int)Math.Ceiling((double)count / UsersPerPage);
            usersModel.Page = actualPage;
            usersModel.Users = mapper.Map<IEnumerable<SingleUserViewModel>>(result);

            return this.PartialView("UsersPartial", usersModel);
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
        [HttpPost]
        public ActionResult UpdateStatus(UserViewModel model)
        {
            var user = this.usersService.GetUserById(model.UserId);

            this.authenticationProvider.ChangeUserRole(user.Id, model.Role);

            this.authenticationProvider.UpdateSecurityStamp(user.Id);

            return this.RedirectToAction("UserProfile", new { id = model.UserId });
        }
    }
}