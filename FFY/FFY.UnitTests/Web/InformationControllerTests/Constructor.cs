using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Web.InformationControllerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new InformationController(null,
                    mockedUsersService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Authentication provider cannot be null.";

            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new InformationController(null,
                    mockedUsersService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new InformationController(mockedAuthenticationProvider.Object,
                    null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Users service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new InformationController(mockedAuthenticationProvider.Object,
                 null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new InformationController(mockedAuthenticationProvider.Object,
                    mockedUsersService.Object));
        }
    }
}
