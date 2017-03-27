using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Controllers;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ShoppingCartControllerTests
{
    [TestFixture]
    public class OrderGet
    {
        [Test]
        public void ShouldRenderDefaultView()
        {
            // Arrange
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
            shoppingCartController.WithCallTo(scs => scs.Order())
                .ShouldRenderDefaultView();
        }
    }
}
