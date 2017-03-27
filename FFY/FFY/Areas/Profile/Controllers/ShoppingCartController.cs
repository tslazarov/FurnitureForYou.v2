using Bytes2you.Validation;
using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Custom.Attributes;
using System.Linq;
using System.Web.Mvc;

namespace FFY.Web.Areas.Profile.Controllers
{
    [Localize]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly ICachingProvider cachingProvider;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly IUsersService usersService;
        private readonly ICartProductsService cartProductsService;
        private readonly IAddressesService addressesService;
        private readonly IOrdersService ordersService;
        private readonly IAddressFactory addressFactory;
        private readonly IOrderFactory orderFactory;

        public ShoppingCartController(IAuthenticationProvider authenticationProvider,
            ICachingProvider cachingProvider,
            IDateTimeProvider dateTimeProvider,
            IShoppingCartsService shoppingCartsService,
            IUsersService usersService,
            ICartProductsService cartProductsService,
            IAddressesService addressesService,
            IOrdersService ordersService,
            IAddressFactory addressFactory,
            IOrderFactory orderFactory)
        {
            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ICachingProvider>(cachingProvider, "Caching provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IDateTimeProvider>(dateTimeProvider, "Date time provider cannot be null.")
                 .IsNull()
                 .Throw();

            Guard.WhenArgument<IShoppingCartsService>(shoppingCartsService, "Shopping carts service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IUsersService>(usersService, "Users service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ICartProductsService>(cartProductsService, "Cart products service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IAddressesService>(addressesService, "Addresses service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IOrdersService>(ordersService, "Orders service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IAddressFactory>(addressFactory, "Address factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IOrderFactory>(orderFactory, "Order factory cannot be null.")
                .IsNull()
                .Throw();

            this.authenticationProvider = authenticationProvider;
            this.cachingProvider = cachingProvider;
            this.dateTimeProvider = dateTimeProvider;
            this.shoppingCartsService = shoppingCartsService;
            this.usersService = usersService;
            this.cartProductsService = cartProductsService;
            this.addressesService = addressesService;
            this.ordersService = ordersService;
            this.addressFactory = addressFactory;
            this.orderFactory = orderFactory;
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
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCartItem(string cartId, int cartProductId)
        {
            var shoppingCart = this.shoppingCartsService.GetShoppingCartById(cartId);
            var cartProduct = this.cartProductsService.GetCartProductById(cartProductId);

            this.shoppingCartsService.Remove(shoppingCart, cartProduct);

            this.cachingProvider.InsertItem($"cart-count-{cartId}", shoppingCart.CartProducts.Where(p => p.IsInCart).Count());

            return this.RedirectToAction("Index", "ShoppingCart");
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
            var paymentStatusType = model.SelectedPaymentType == "1" ?
                OrderPaymentStatusType.PaymentOnDelivery : OrderPaymentStatusType.Payed;
            var orderStatusType = OrderStatusType.Processing;

            var user = this.usersService.GetUserById(this.authenticationProvider.CurrentUserId);
            var shoppingCart = user.ShoppingCart;
            var address = this.addressFactory.CreateAddress(model.Street, 
                model.City, 
                model.Country);

            this.addressesService.AddAddress(address);

            var order = this.orderFactory.CreateOrder(user.Id,
                user,
                this.dateTimeProvider.GetCurrentTime(),
                shoppingCart.Total,
                address.Id,
                address,
                model.PhoneNumber,
                paymentStatusType,
                orderStatusType);

            this.ordersService.TransferProducts(order, shoppingCart);

            this.ordersService.AddOrder(order);

            this.shoppingCartsService.Clear(shoppingCart);

            this.cachingProvider.InsertItem($"cart-count-{user.Id}", 0);

            return this.View("OrderComplete");
        }
    }
}