using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Web.ContactManagementControllerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ContactManagementController(null,
                mockedMapperProvider.Object,
                mockedContactsService.Object,
                mockedUsersService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Authentication provider cannot be null.";

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            new ContactManagementController(null,
                mockedMapperProvider.Object,
                mockedContactsService.Object,
                mockedUsersService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ContactManagementController(mockedAuthenticationProvider.Object,
                null,
                mockedContactsService.Object,
                mockedUsersService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Mapper provider cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            new ContactManagementController(mockedAuthenticationProvider.Object,
                null,
                mockedContactsService.Object,
                mockedUsersService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContactsServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ContactManagementController(mockedAuthenticationProvider.Object,
                mockedMapperProvider.Object,
                null,
                mockedUsersService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContactsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Contacts service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            new ContactManagementController(mockedAuthenticationProvider.Object,
                mockedMapperProvider.Object,
                null,
                mockedUsersService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ContactManagementController(mockedAuthenticationProvider.Object,
                mockedMapperProvider.Object,
                mockedContactsService.Object,
                null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Users service cannot be null.";

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            new ContactManagementController(mockedAuthenticationProvider.Object,
                mockedMapperProvider.Object,
                mockedContactsService.Object,
                null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            var mockedUsersService = new Mock<IUsersService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new ContactManagementController(mockedAuthenticationProvider.Object,
                mockedMapperProvider.Object,
                mockedContactsService.Object,
                mockedUsersService.Object));
        }
    }
}
