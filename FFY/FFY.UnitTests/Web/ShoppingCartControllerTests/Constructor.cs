using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Controllers;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Web.ShoppingCartControllerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(null,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Authentication provider cannot be null.";
            
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(null,
                    mockedCachingProvider.Object,
                    mockedDateTimeProvider.Object,
                    mockedShoppingCartsService.Object,
                    mockedUsersService.Object,
                    mockedCartProductsService.Object,
                    mockedAddressesService.Object,
                    mockedOrdersService.Object,
                    mockedAddressFactory.Object,
                    mockedOrderFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCachingProviderIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        null,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCachingProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Caching provider cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(mockedAuthenticationProvider.Object,
                    null,
                    mockedDateTimeProvider.Object,
                    mockedShoppingCartsService.Object,
                    mockedUsersService.Object,
                    mockedCartProductsService.Object,
                    mockedAddressesService.Object,
                    mockedOrdersService.Object,
                    mockedAddressFactory.Object,
                    mockedOrderFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullDateTimeProviderIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        null,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDateTimeProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Date time provider cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    null,
                    mockedShoppingCartsService.Object,
                    mockedUsersService.Object,
                    mockedCartProductsService.Object,
                    mockedAddressesService.Object,
                    mockedOrdersService.Object,
                    mockedAddressFactory.Object,
                    mockedOrderFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullShoppingCartsServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        null,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullShoppingCartsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Shopping carts service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedDateTimeProvider.Object,
                    null,
                    mockedUsersService.Object,
                    mockedCartProductsService.Object,
                    mockedAddressesService.Object,
                    mockedOrdersService.Object,
                    mockedAddressFactory.Object,
                    mockedOrderFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        null,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Users service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedDateTimeProvider.Object,
                    mockedShoppingCartsService.Object,
                    null,
                    mockedCartProductsService.Object,
                    mockedAddressesService.Object,
                    mockedOrdersService.Object,
                    mockedAddressFactory.Object,
                    mockedOrderFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCartProductsServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        null,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCartProductsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Cart products service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedDateTimeProvider.Object,
                    mockedShoppingCartsService.Object,
                    mockedUsersService.Object,
                    null,
                    mockedAddressesService.Object,
                    mockedOrdersService.Object,
                    mockedAddressFactory.Object,
                    mockedOrderFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAddressesServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        null,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAddressesServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Addresses service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedDateTimeProvider.Object,
                    mockedShoppingCartsService.Object,
                    mockedUsersService.Object,
                    mockedCartProductsService.Object,
                    null,
                    mockedOrdersService.Object,
                    mockedAddressFactory.Object,
                    mockedOrderFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullOrdersServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        null,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullOrdersServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Orders service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedDateTimeProvider.Object,
                    mockedShoppingCartsService.Object,
                    mockedUsersService.Object,
                    mockedCartProductsService.Object,
                    mockedAddressesService.Object,
                    null,
                    mockedAddressFactory.Object,
                    mockedOrderFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAddressFactoryIsPassed()
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
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        null,
                        mockedOrderFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAddressFactoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Address factory cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedDateTimeProvider.Object,
                    mockedShoppingCartsService.Object,
                    mockedUsersService.Object,
                    mockedCartProductsService.Object,
                    mockedAddressesService.Object,
                    mockedOrdersService.Object,
                    null,
                    mockedOrderFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullOrderFactoryIsPassed()
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

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullOrderFactoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Order factory cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedDateTimeProvider.Object,
                    mockedShoppingCartsService.Object,
                    mockedUsersService.Object,
                    mockedCartProductsService.Object,
                    mockedAddressesService.Object,
                    mockedOrdersService.Object,
                    mockedAddressFactory.Object,
                    null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
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

            // Act and Assert
            Assert.DoesNotThrow(() =>
                    new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object));
        }
    }
}
