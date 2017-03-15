using FFY.Data.Factories;
using FFY.IdentityConfig.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Web.AccountControllerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AccountController(null,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Authentication provider cannot be null.";

            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AccountController(null,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserFactoryIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AccountController(mockedAuthenticationProvider.Object,
                    null,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserFactoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "User factory cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AccountController(mockedAuthenticationProvider.Object,
                    null,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullShoppingCartFactoryIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    null,
                    mockedShoppingCartsService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullShoppingCartFactoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Shopping cart factory cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    null,
                    mockedShoppingCartsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullShoppingCartsServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullShoppingCartsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Shopping carts service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUserFactory = new Mock<IUserFactory>();
            var mockedShoppingCartFactory = new Mock<IShoppingCartFactory>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new AccountController(mockedAuthenticationProvider.Object,
                    mockedUserFactory.Object,
                    mockedShoppingCartFactory.Object,
                    mockedShoppingCartsService.Object));
        }
    }
}
