using Bytes2you.Validation;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Custom.Attributes;
using FFY.Web.Mappings;
using FFY.Web.Models.Furniture;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    [Localize]
    public class FurnitureController : Controller
    {
        private const int ProductsPerPage = 16;

        private readonly IAuthenticationProvider authenticationProvider;
        private readonly ICachingProvider cachingProvider;
        private readonly IMapperProvider mapper;
        private readonly IUsersService usersService;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly IProductsService productsService;
        private readonly IRoomsService roomsService;

        public FurnitureController(IAuthenticationProvider authenticationProvider,
            ICachingProvider cachingProvider,
            IMapperProvider mapper,
            IUsersService usersService,
            IShoppingCartsService shoppingCartsService,
            IProductsService productsService,
            IRoomsService roomsService)
        {
            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ICachingProvider>(cachingProvider, "Caching provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
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

            Guard.WhenArgument<IRoomsService>(roomsService, "Rooms service cannot be null.")
                .IsNull()
                .Throw();

            this.authenticationProvider = authenticationProvider;
            this.cachingProvider = cachingProvider;
            this.mapper = mapper;
            this.usersService = usersService;
            this.shoppingCartsService = shoppingCartsService;
            this.productsService = productsService;
            this.roomsService = roomsService;
        }

        public ActionResult Index(RoomsViewModel roomsViewModel)
        {
            roomsViewModel.Rooms = this.roomsService.GetRooms();

            return this.View(roomsViewModel);
        }


        // GET: Furniture/FilterParamameter
        public ActionResult Products(ProductsSelectionViewModel productsSelectionViewModel,
            string filterBy,
            string search,
            int? from,
            int? to,
            int? page)
        {
            var actualPage = page ?? 1;

            var result = this.productsService.GetProductsSelection(filterBy,
                search,
                from,
                to,
                actualPage,
                ProductsPerPage);

            var count = this.productsService.GetProductsSelectionCount(filterBy,
                search,
                from,
                to);

            productsSelectionViewModel.FilterBy = filterBy;
            productsSelectionViewModel.Search = search;
            productsSelectionViewModel.From = from;
            productsSelectionViewModel.To = to;
            productsSelectionViewModel.ProductsCount = count;
            productsSelectionViewModel.Pages = (int)Math.Ceiling((double)count / ProductsPerPage);
            productsSelectionViewModel.Page = actualPage;
            productsSelectionViewModel.Products =
                this.mapper.Map<IEnumerable<SingleProductSelectionViewModel>>(result);

            return this.View(productsSelectionViewModel);
        }

        // GET: Furniture/Product
        public ActionResult Product(int id, DetailedProductViewModel model)
        {
            model.Product = this.productsService.GetProductById(id);
            model.Quantity = 1;
            model.GivenRating = 1;
            model.QuantityExceeded = false;

            this.ModelState.Clear();

            if (model.Product == null)
            {
                return this.View("NotFound");
            }

            return this.View(model);
        }

        // POST: Furniture/AddShoppingCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddShoppingCart(DetailedProductViewModel model)
        {
            if (!this.authenticationProvider.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account", new { returnUrl = $"/furniture/product/{model.Product.Id}" });
            }

            var user = this.usersService.GetUserById(this.authenticationProvider.CurrentUserId);
            var product = this.productsService.GetProductById(model.Product.Id);

            if (product.Quantity < model.Quantity)
            {
                model.Product = product;
                model.QuantityExceeded = true;

                return this.View("Product", model);
            }

            var shoppingCart = user.ShoppingCart;

            this.shoppingCartsService.Add(shoppingCart, product, model.Quantity);
            var cartCount = this.shoppingCartsService.CartProductsCount(shoppingCart.UserId);

            this.cachingProvider.InsertItem($"cart-count-{user.Id}", cartCount);

            return this.RedirectToAction("Product", "Furniture", new { id = model.Product.Id });

        }

        // POST: Furniture/Rate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rate(DetailedProductViewModel model)
        {
            if (!this.authenticationProvider.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account", new { returnUrl = $"/furniture/product/{model.Product.Id}" });
            }

            var user = this.usersService.GetUserById(this.authenticationProvider.CurrentUserId);
            var product = this.productsService.GetProductById(model.Product.Id);

            this.usersService.RateProduct(user, product, model.GivenRating);

            return this.RedirectToAction("Product", "Furniture", new { id = model.Product.Id });
        }

        // POST: Furniture/AddFavorites
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFavorites(DetailedProductViewModel model)
        {
            if (!this.authenticationProvider.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account", new { returnUrl = $"/furniture/product/{model.Product.Id}" });
            }

            var user = this.usersService.GetUserById(this.authenticationProvider.CurrentUserId);
            var product = this.productsService.GetProductById(model.Product.Id);

            this.usersService.AddProductToFavorites(user, product);

            this.cachingProvider.InsertItem($"favorites-count-{user.Id}", user.FavoritedProducts.Count);

            return this.RedirectToAction("Product", "Furniture", new { id = model.Product.Id });
        }

        // POST: Furniture/RemoveFavorites
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFavorites(DetailedProductViewModel model)
        {

            if (!this.authenticationProvider.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account", new { returnUrl = $"/furniture/product/{model.Product.Id}" });
            }

            var user = this.usersService.GetUserById(this.authenticationProvider.CurrentUserId);
            var product = this.productsService.GetProductById(model.Product.Id);

            this.usersService.RemoveProductFromFavorites(user, product);

            this.cachingProvider.InsertItem($"favorites-count-{user.Id}", user.FavoritedProducts.Count);

            return this.RedirectToAction("Product", "Furniture", new { id = model.Product.Id });
        }
    }
}