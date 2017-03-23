using Bytes2you.Validation;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Custom.Attributes;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Profile.Controllers
{
    [Localize]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly ICachingProvider cachingProvider;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly ICartProductsService cartProductsService;

        public ShoppingCartController(IAuthenticationProvider authenticationProvider,
            ICachingProvider cachingProvider,
            IShoppingCartsService shoppingCartsService,
            ICartProductsService cartProductsService)
        {
            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ICachingProvider>(cachingProvider, "Caching provider cannot be null")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IShoppingCartsService>(shoppingCartsService, "Shopping carts service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ICartProductsService>(cartProductsService, "Cart products service cannot be null.")
                .IsNull()
                .Throw();

            this.authenticationProvider = authenticationProvider;
            this.cachingProvider = cachingProvider;
            this.shoppingCartsService = shoppingCartsService;
            this.cartProductsService = cartProductsService;
        }

        // GET: Profile/ShoppingCart
        public ViewResult Index(ShoppingCartViewModel model)
        {
            var cartId = this.authenticationProvider.CurrentUserId;

            model.ShoppingCart = this.shoppingCartsService.GetShoppingCartById(cartId);

            return this.View(model);
        }

        // POST: Profile/RemoveCartItem
        [HttpPost]
        public ActionResult RemoveCartItem(string cartId, int cartProductId)
        {
            var shoppingCart = this.shoppingCartsService.GetShoppingCartById(cartId);
            var cartProduct = this.cartProductsService.GetCartProductById(cartProductId);

            this.shoppingCartsService.Remove(shoppingCart, cartProduct);

            this.cachingProvider.InsertItem($"cart-count-{cartId}", shoppingCart.CartProducts.Where(p => p.IsInCart).Count());

            return this.RedirectToAction("index", "shoppingCart");
        }

        // GET: Profile/ShoppingCart/Order
        public ViewResult Order()
        {
            return this.View();
        }

        // POST: Profile/ShoppingCart/Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(OrderViewModel model)
        {
            var test = model;
            return this.View();
        }
    }
}