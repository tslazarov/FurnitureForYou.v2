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
        private readonly IHttpContextProvider contextProvider;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly ICartProductsService cartProductsService;

        public ShoppingCartController(IHttpContextProvider contextProvider,
            IShoppingCartsService shoppingCartsService,
            ICartProductsService cartProductsService)
        {
            Guard.WhenArgument<IHttpContextProvider>(contextProvider, "Context provider cannot be null")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IShoppingCartsService>(shoppingCartsService, "Shopping carts service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ICartProductsService>(cartProductsService, "Cart products service cannot be null.")
                .IsNull()
                .Throw();

            this.contextProvider = contextProvider;
            this.shoppingCartsService = shoppingCartsService;
            this.cartProductsService = cartProductsService;
        }

        // GET: Profile/ShoppingCart
        public ViewResult Index(ShoppingCartViewModel model)
        {
            var cartId = this.User.Identity.GetUserId();

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

            this.contextProvider.InsertInCache(this, $"cart-count-{cartId}", shoppingCart.CartProducts.Count);

            return this.RedirectToAction("index", "shoppingCart");
        }
    }
}