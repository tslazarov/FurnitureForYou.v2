using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.ShoppingCartsServiceTests
{
    [TestFixture]
    public class AssignShoppingCart
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullShoppingCartIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                shoppingCartsService.AssignShoppingCart(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullShoppingCartIsPassed()
        {
            // Arrange
            var expectedExMessage = "Shopping cart cannot be null.";
            var mockedData = new Mock<IFFYData>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                shoppingCartsService.AssignShoppingCart(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallAddMethodOfDataShoppingCartRepository()
        {
            // Arrange
            var mockedShoppingCart = new Mock<ShoppingCart>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ShoppingCartsRepository.Add(It.IsAny<ShoppingCart>()))
                .Verifiable();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object);

            // Act
            shoppingCartsService.AssignShoppingCart(mockedShoppingCart.Object);

            // Assert
            mockedData.Verify(d => 
                d.ShoppingCartsRepository.Add(mockedShoppingCart.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedShoppingCart = new Mock<ShoppingCart>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ShoppingCartsRepository.Add(It.IsAny<ShoppingCart>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object);

            // Act
            shoppingCartsService.AssignShoppingCart(mockedShoppingCart.Object);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
