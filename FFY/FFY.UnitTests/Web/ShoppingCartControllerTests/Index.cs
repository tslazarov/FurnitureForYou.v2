using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Controllers;
using FFY.Web.Areas.Profile.Models;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ShoppingCartControllerTests
{
    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldGetCurrentUserIdPropertyOfAuthenticationProvider()
        {
            // Arrange
            var shoppingCartViewModel = new ShoppingCartViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId).Verifiable();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
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
            shoppingCartController.Index(shoppingCartViewModel);

            // Assert
            mockedAuthenticationProvider.VerifyGet(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void ShouldCallGetShoppingCartByIdMethodOfShoppingCartsService()
        {
            // Arrange
            var id = "42";
            var shoppingCartViewModel = new ShoppingCartViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs => scs.GetShoppingCartById(It.IsAny<string>()))
                .Verifiable();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
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
            shoppingCartController.Index(shoppingCartViewModel);

            // Assert
            mockedShoppingCartsService.Verify(scs => scs.GetShoppingCartById(id), Times.Once);
        }

        [Test]
        public void ShouldReturnDefaultViewWithShoppingCartViewModel()
        {
            // Arrange
            var shoppingCartViewModel = new ShoppingCartViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
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

            // Act and Assert
            shoppingCartController.WithCallTo(scs => scs.Index(shoppingCartViewModel))
                .ShouldRenderDefaultView()
                .WithModel<ShoppingCartViewModel>(model => Assert.AreEqual(shoppingCartViewModel, model));
        }
    }
}
