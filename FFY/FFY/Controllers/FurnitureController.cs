using Bytes2you.Validation;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Custom.Attributes;
using FFY.Web.Models.Furniture;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    [Localize]
    public class FurnitureController : Controller
    {
        private readonly IHttpContextProvider contextProvider;
        private readonly IUsersService usersService;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly IProductsService productsService;

        public FurnitureController(IHttpContextProvider contextProvider,
            IUsersService usersService,
            IShoppingCartsService shoppingCartsService,
            IProductsService productsService)
        {
            Guard.WhenArgument<IHttpContextProvider>(contextProvider, "Contect provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IUsersService>(usersService, "Users service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IShoppingCartsService>(shoppingCartsService, "Shopping carts service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IProductsService>(productsService, "Products service cannot be null.")
                .IsNull()
                .Throw();

            this.contextProvider = contextProvider;
            this.usersService = usersService;
            this.shoppingCartsService = shoppingCartsService;
            this.productsService = productsService;
        }

        // GET: Furniture/Product
        public ActionResult Product(int? id, DetailedProductViewModel model)
        {
            model.Product = this.productsService.GetProductById(id.Value);
            model.Quantity = 1;
            model.GivenRating = 1;

            this.ModelState.Clear();

            if (model.Product == null)
            {
                // 404
            }

            return this.View(model);
        }

        // POST: Furniture/AddShoppingCart
        [HttpPost]
        public ActionResult AddShoppingCart(DetailedProductViewModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("login", "account", new { returnUrl = $"/furniture/product/{model.Product.Id}" });
            }

            var user = this.usersService.GetUserById(this.User.Identity.GetUserId());
            var product = this.productsService.GetProductById(model.Product.Id);
            var shoppingCart = user.ShoppingCart;

            this.shoppingCartsService.Add(shoppingCart, product, model.Quantity);
            var cartCount = this.shoppingCartsService.CartProductsCount(shoppingCart.UserId);

            this.contextProvider.GetHttpContext(this).Cache.Insert($"cart-count-{user.Id}", cartCount);

            return this.RedirectToAction("product", "furniture", new { id = model.Product.Id });

        }

        // POST: Furniture/Rate
        [HttpPost]
        public ActionResult Rate(DetailedProductViewModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("login", "account", new { returnUrl = $"/furniture/product/{model.Product.Id}" });
            }

            var user = this.usersService.GetUserById(this.User.Identity.GetUserId());
            var product = this.productsService.GetProductById(model.Product.Id);

            this.usersService.RateProduct(user, product, model.GivenRating);

            return this.RedirectToAction("product", "furniture", new { id = model.Product.Id });
        }

        // POST: Furniture/AddFavorites
        [HttpPost]
        public ActionResult AddFavorites(DetailedProductViewModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("login", "account", new { returnUrl = $"/furniture/product/{model.Product.Id}" });
            }

            var user = this.usersService.GetUserById(this.User.Identity.GetUserId());
            var product = this.productsService.GetProductById(model.Product.Id);

            this.usersService.AddProductToFavorites(user, product);

            this.contextProvider.GetHttpContext(this).Cache.Insert($"favorites-count-{user.Id}", user.FavoritedProducts.Count + 1);

            return this.RedirectToAction("product", "furniture", new { id = model.Product.Id });
        }

        // POST: Furniture/RemoveFavorites
        [HttpPost]
        public ActionResult RemoveFavorites(DetailedProductViewModel model)
        {

            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("login", "account", new { returnUrl = $"/furniture/product/{model.Product.Id}" });
            }

            var user = this.usersService.GetUserById(this.User.Identity.GetUserId());
            var product = this.productsService.GetProductById(model.Product.Id);

            this.usersService.RemoveProductFromFavorites(user, product);

            this.contextProvider.GetHttpContext(this).Cache.Insert($"favorites-count-{user.Id}", user.FavoritedProducts.Count - 1);

            return this.RedirectToAction("product", "furniture", new { id = model.Product.Id });
        }
    }
}