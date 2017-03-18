using FFY.Data.Contracts;
using FFY.Data.Factories;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Services.ShoppingCartsServiceTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullDataIsPassed()
        {
            // Arrange
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartsService(null, mockedCartProductFactory.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDataIsPassed()
        {
            // Arrange
            var expectedExMessage = "Data cannot be null.";
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartsService(null, mockedCartProductFactory.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCartProductFactoryIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartsService(mockedData.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCartProductFactoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Cart product factory cannot be null.";
            var mockedData = new Mock<IFFYData>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ShoppingCartsService(mockedData.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidDependenciesArePassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new ShoppingCartsService(mockedData.Object, mockedCartProductFactory.Object));
        }
    }
}
