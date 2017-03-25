using Bytes2you.Validation;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Custom.Attributes;
using System.Web.Mvc;

namespace FFY.Web.Areas.Profile.Controllers
{
    [Localize]
    [Authorize]
    public class InformationController : Controller
    {
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IUsersService usersService;

        public InformationController(IAuthenticationProvider authenticationProvider,
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


        // GET: Profile/Information
        public ViewResult Index(ProfileViewModel model)
        {
            model.User = this.usersService.GetUserById(this.authenticationProvider.CurrentUserId);

            return this.View(model);
        }
    }
}