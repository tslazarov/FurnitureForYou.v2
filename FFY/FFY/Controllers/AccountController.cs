﻿using Bytes2you.Validation;
using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Resources;
using FFY.Services.Contracts;
using FFY.Web.Custom.Attributes;
using FFY.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    [Authorize]
    [Localize]
    public class AccountController : Controller
    {
        private readonly ICachingProvider cachingProvider;
        private readonly IRouteDataProvider routeDataProvider;
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IUserFactory userFactory;
        private readonly IShoppingCartFactory shoppingCartFactory;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly IUsersService usersService;

        public AccountController()
        {
        }

        public AccountController(ICachingProvider cachingProvider,
            IRouteDataProvider routeDataProvider,
            IAuthenticationProvider authenticationProvider, 
            IUserFactory userFactory,
            IShoppingCartFactory shoppingCartFactory,
            IShoppingCartsService shoppingCartsService,
            IUsersService usersService)
        {
            Guard.WhenArgument<ICachingProvider>(cachingProvider, "Caching provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IRouteDataProvider>(routeDataProvider, "Route data provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IUserFactory>(userFactory, "User factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IShoppingCartFactory>(shoppingCartFactory, "Shopping cart factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IShoppingCartsService>(shoppingCartsService, "Shopping carts service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IUsersService>(usersService, "Users service cannot be null.")
                .IsNull()
                .Throw();

            this.cachingProvider = cachingProvider;
            this.routeDataProvider = routeDataProvider;
            this.authenticationProvider = authenticationProvider;
            this.userFactory = userFactory;
            this.shoppingCartFactory = shoppingCartFactory;
            this.shoppingCartsService = shoppingCartsService;
            this.usersService = usersService;
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var routeData = this.routeDataProvider.GetRouteData(this);

            returnUrl = string.IsNullOrEmpty(returnUrl) ? 
                $"/{routeData.Values["language"].ToString()}" : $"/{routeData.Values["language"].ToString()}{returnUrl}";

            var result = this.authenticationProvider.SignInWithPassword(model.Email, model.Password, false, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    var user = this.usersService.GetUserByEmail(model.Email);

                    this.cachingProvider.InsertItem($"favorites-count-{user.Id}", user.FavoritedProducts.Count);
                    this.cachingProvider.InsertItem($"cart-count-{user.Id}", user.ShoppingCart.CartProducts.Where(p => p.IsInCart).Count());

                    return this.Redirect(returnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", Language.InvalidLogin);
                    return this.View(model);
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ViewResult Register()
        {
            return this.View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = this.userFactory.CreateUser(model.Email, model.FirstName, model.LastName, model.Email);
                var result = this.authenticationProvider.CreateUser(user, model.Password);
                var routeData = this.routeDataProvider.GetRouteData(this);

                if (result.Succeeded)
                {
                    var shoppingCart = this.shoppingCartFactory.CreateShoppingCart(user.Id, user);

                    this.shoppingCartsService.AssignShoppingCart(shoppingCart);

                    this.authenticationProvider.SignIn(user, isPersistent:false, rememberBrowser:false);

                    return this.RedirectToAction("Index", "Home", new { area = "", language = routeData.Values["language"].ToString() });
                }

                this.AddErrors(result);
            }

            return this.View(model);
        }

        // POST: /Account/LogOut
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            var routeData = this.routeDataProvider.GetRouteData(this);
            this.authenticationProvider.SignOut();

            return this.RedirectToAction("Index", "Home", new { area = "", language = routeData.Values["language"].ToString() });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError("", error);
            }
        }
    }
}