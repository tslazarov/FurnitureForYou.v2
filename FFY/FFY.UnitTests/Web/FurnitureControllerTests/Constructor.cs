using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Web.FurnitureControllerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(null,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));

        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Authentication provider cannot be null.";

            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(null,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCachingProviderIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    null,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));

        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCachingProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Caching provider cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    null,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    null,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));

        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Mapper provider cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    null,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    null,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));

        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Users service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    null,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullShoppingCartsServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    null,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));

        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullShoppingCartsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Shopping carts service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    null,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullProductsServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    null,
                    mockedRoomsService.Object));

        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullProductsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Products service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    null,
                    mockedRoomsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullRoomsServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    null));

        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullRoomsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Rooms service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedMapper = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomsService = new Mock<IRoomsService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new FurnitureController(mockedAuthenticationProvider.Object,
                    mockedCachingProvider.Object,
                    mockedMapper.Object,
                    mockedUsersService.Object,
                    mockedShoppingCartsService.Object,
                    mockedProductsService.Object,
                    mockedRoomsService.Object));

        }
    }
}
