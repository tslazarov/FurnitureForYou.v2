using Bytes2you.Validation;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Custom.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Profile.Controllers
{
    [Localize]
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IUsersService usersService;

        public FavoritesController(IAuthenticationProvider authenticationProvider,
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


        // GET: Profile/Favorites
        public ActionResult Index(FavoritesViewModel model)
        {
            var user = this.usersService.GetUserById(this.authenticationProvider.CurrentUserId);

            model.FavoritedProducts = user.FavoritedProducts;

            return this.View(model);
        }
    }
}