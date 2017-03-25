using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Web.OrderManagementControllerTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var mockedOrdersService = new Mock<IOrdersService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new OrderManagementController(null,
                    mockedOrdersService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Mapper provider cannot be null.";

            var mockedOrdersService = new Mock<IOrdersService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new OrderManagementController(null,
                    mockedOrdersService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullOrdersServiceIsPassed()
        {
            // Arrange
            var mockedMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new OrderManagementController(mockedMapperProvider.Object,
                    null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullOrdersServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Orders service cannot be null.";

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new OrderManagementController(mockedMapperProvider.Object,
                    null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new OrderManagementController(mockedMapperProvider.Object,
                    mockedOrdersService.Object));
        }
    }
}
