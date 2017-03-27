using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Controllers;
using FFY.Web.Areas.Profile.Models;
using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ShoppingCartControllerTests
{
    [TestFixture]
    public class OrderPost
    {
        [Test]
        public void ShouldGetCurrentUserIdPropertyOfAuthenticationProvider()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var orderViewModel = new OrderViewModel() { SelectedPaymentType = "1" };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id)
                .Verifiable();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedAuthenticationProvider.VerifyGet(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void ShouldCallGetUserByIdMethodOfUsersService()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var orderViewModel = new OrderViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedUsersService.Verify(us => us.GetUserById(id), Times.Once);
        }

        [Test]
        public void ShouldCallCreateAddressMethodOfAddressFactory()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var orderViewModel = new OrderViewModel() {
                Street = "Street",
                City = "City",
                Country = "Country"
            };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address)
                .Verifiable();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedAddressFactory.Verify(af => 
                af.CreateAddress(orderViewModel.Street,
                    orderViewModel.City,
                    orderViewModel.Country), Times.Once);
        }

        [Test]
        public void ShouldCallAddAddressMethodOfAddressesService()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var orderViewModel = new OrderViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            mockedAddressesService.Setup(ads => ads.AddAddress(It.IsAny<Address>()))
                .Verifiable();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedAddressesService.Verify(ads => ads.AddAddress(address), Times.Once);
        }

        [Test]
        public void ShouldCallGetCurrentTimeMethodOfDateTimeProvider()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var orderViewModel = new OrderViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Verifiable();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedDateTimeProvider.Verify(dtp => dtp.GetCurrentTime(), Times.Once);
        }

        [Test]
        public void ShouldCallCreateOrderMethodOfOrderFactory()
        {
            // Arrange
            var date = new DateTime(2017, 3, 27);
            var id = "42";
            var user = new User() { Id = id, ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var orderViewModel = new OrderViewModel() { PhoneNumber = "123" };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Returns(date);
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();
            mockedOrderFactory.Setup(of => of.CreateOrder(It.IsAny<string>(),
                It.IsAny<User>(),
                It.IsAny<DateTime>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<Address>(),
                It.IsAny<string>(),
                It.IsAny<OrderPaymentStatusType>(),
                It.IsAny<OrderStatusType>()));

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedOrderFactory.Verify(of => of.CreateOrder(id,
                user,
                date,
                user.ShoppingCart.Total,
                address.Id,
                address,
                orderViewModel.PhoneNumber,
                It.IsAny<OrderPaymentStatusType>(),
                It.IsAny<OrderStatusType>()), Times.Once);
        }


        [Test]
        public void ShouldCallTransferProductsMethodOfOrdersService()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var order = new Order();
            var orderViewModel = new OrderViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Verifiable();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.TransferProducts(It.IsAny<Order>(),
                It.IsAny<ShoppingCart>()))
                .Verifiable();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();
            mockedOrderFactory.Setup(of => of.CreateOrder(It.IsAny<string>(),
                It.IsAny<User>(),
                It.IsAny<DateTime>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<Address>(),
                It.IsAny<string>(),
                It.IsAny<OrderPaymentStatusType>(),
                It.IsAny<OrderStatusType>()))
                .Returns(order);

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedOrdersService.Verify(os => os.TransferProducts(order, user.ShoppingCart), Times.Once);
        }

        [Test]
        public void ShouldCallAddOrderMethodOfOrdersService()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var order = new Order();
            var address = new Address();
            var orderViewModel = new OrderViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.AddOrder(It.IsAny<Order>()));
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();
            mockedOrderFactory.Setup(of => of.CreateOrder(It.IsAny<string>(),
                It.IsAny<User>(),
                It.IsAny<DateTime>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<Address>(),
                It.IsAny<string>(),
                It.IsAny<OrderPaymentStatusType>(),
                It.IsAny<OrderStatusType>()))
                .Returns(order);

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedOrdersService.Verify(os => os.AddOrder(order), Times.Once);
        }

        [Test]
        public void ShouldCallClearMethodOfShoppingCartsService()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var orderViewModel = new OrderViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs => scs.Clear(It.IsAny<ShoppingCart>()))
                .Verifiable();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.AddOrder(It.IsAny<Order>()));
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedShoppingCartsService.Verify(scs => scs.Clear(user.ShoppingCart), Times.Once);
        }

        [Test]
        public void ShouldCallInsertItemMethodOfCachingProvider()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var orderViewModel = new OrderViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            mockedCachingProvider.Setup(cp => cp.InsertItem(It.IsAny<string>(),
                It.IsAny<int>()))
                .Verifiable();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.AddOrder(It.IsAny<Order>()));
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.Order(orderViewModel);

            // Assert
            mockedCachingProvider.Verify(cp => cp.InsertItem(It.IsAny<string>(), 0), Times.Once);
        }

        [Test]
        public void ShouldReturnOrderCompleteView()
        {
            // Arrange
            var id = "42";
            var user = new User() { ShoppingCart = new ShoppingCart() };
            var address = new Address();
            var orderViewModel = new OrderViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.AddOrder(It.IsAny<Order>()));
            var mockedAddressFactory = new Mock<IAddressFactory>();
            mockedAddressFactory.Setup(af => af.CreateAddress(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(address);
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.WithCallTo(scc => scc.Order(orderViewModel))
                .ShouldRenderView("OrderComplete");
        }
    }
}
