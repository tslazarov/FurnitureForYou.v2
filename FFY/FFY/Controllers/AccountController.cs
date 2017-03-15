using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FFY.IdentityConfig.Contracts;
using FFY.Data.Factories;
using Bytes2you.Validation;
using FFY.Web.Models.Account;
using FFY.Data.Contracts;
using FFY.Services.Contracts;

namespace FFY.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private const string XsrfKey = "XsrfId";

        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IUserFactory userFactory;
        private readonly IShoppingCartFactory shoppingCartFactory;
        private readonly IShoppingCartsService shoppingCartsService;

        public AccountController()
        {
        }

        public AccountController(IAuthenticationProvider provider, 
            IUserFactory userFactory,
            IShoppingCartFactory shoppingCartFactory,
            IShoppingCartsService shoppingCartsService)
        {
            Guard.WhenArgument<IAuthenticationProvider>(provider, "Authentication provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IUserFactory>(userFactory, "User factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IShoppingCartsService>(shoppingCartsService, "Shopping carts service cannot be null.")
                .IsNull()
                .Throw();

            this.authenticationProvider = provider;
            this.userFactory = userFactory;
            this.shoppingCartFactory = shoppingCartFactory;
            this.shoppingCartsService = shoppingCartsService;
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/Home/Index" : returnUrl;

            var result = this.authenticationProvider.SignInWithPassword(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return this.Redirect(returnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return this.View(model);
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.userFactory.CreateUser(model.Email, model.FirstName, model.LastName, model.Email);
                var result = this.authenticationProvider.CreateUser(user, model.Password);

                if (result.Succeeded)
                {
                    var shoppingCart = this.shoppingCartFactory.CreateShoppingCart(user.Id, user);

                    this.shoppingCartsService.AssignShoppingCart(shoppingCart);

                    this.authenticationProvider.SignIn(user, isPersistent:false, rememberBrowser:false);
                    
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            return this.View(model);
        }

        // POST: /Account/LogOut
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            this.authenticationProvider.SignOut();

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}