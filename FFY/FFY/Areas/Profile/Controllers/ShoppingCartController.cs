using Bytes2you.Validation;
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
        private readonly IShoppingCartsService shoppingCartsService;

        public ShoppingCartController(IShoppingCartsService shoppingCartsService)
        {
            Guard.WhenArgument<IShoppingCartsService>(shoppingCartsService, "Shopping carts service cannot be null.")
                .IsNull()
                .Throw();

            this.shoppingCartsService = shoppingCartsService;
        }

        // GET: Profile/ShoppingCart
        public ViewResult Index(ShoppingCartViewModel model)
        {
            var cartId = this.User.Identity.GetUserId();

            model.ShoppingCart = this.shoppingCartsService.GetShoppingCartById(cartId);

            return this.View(model);
        }
    }
}