using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Web.ContactControllerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullDateTimeProviderIsPassed()
        {
            // Arrange
            var mockedContactFactory = new Mock<IContactFactory>();
            var mockedContactsService = new Mock<IContactsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ContactController(null,
                mockedContactFactory.Object,
                mockedContactsService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDateTimeProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Date time provider cannot be null.";

            var mockedContactFactory = new Mock<IContactFactory>();
            var mockedContactsService = new Mock<IContactsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ContactController(null,
                mockedContactFactory.Object,
                mockedContactsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContactFactoryIsPassed()
        {
            // Arrange
            var mockedDateProvider = new Mock<IDateTimeProvider>();
            var mockedContactsService = new Mock<IContactsService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ContactController(mockedDateProvider.Object,
                null,
                mockedContactsService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContactFactoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Contact factory cannot be null.";

            var mockedDateProvider = new Mock<IDateTimeProvider>();
            var mockedContactsService = new Mock<IContactsService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ContactController(mockedDateProvider.Object,
                null,
                mockedContactsService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContactsServiceIsPassed()
        {
            // Arrange
            var mockedDateProvider = new Mock<IDateTimeProvider>();
            var mockedContactFactory = new Mock<IContactFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ContactController(mockedDateProvider.Object,
                mockedContactFactory.Object,
                null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContactsServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Contacts service cannot be null.";

            var mockedDateProvider = new Mock<IDateTimeProvider>();
            var mockedContactFactory = new Mock<IContactFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ContactController(mockedDateProvider.Object,
                mockedContactFactory.Object,
                null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedDateProvider = new Mock<IDateTimeProvider>();
            var mockedContactFactory = new Mock<IContactFactory>();
            var mockedContactsService = new Mock<IContactsService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new ContactController(mockedDateProvider.Object,
                mockedContactFactory.Object,
                mockedContactsService.Object));
        }
    }
}
