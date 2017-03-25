using FFY.Data.Contracts;
using FFY.Data.Factories;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.ShoppingCartsServiceTests
{
    [TestFixture]
    public class GetShoppingCartById
    {
        [Test]
        public void ShouldThrowArgumentException_WhenEmptyCartIdIsPassed()
        {
            // Arrange
            var cartId = "";
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                shoppingCartsService.GetShoppingCartById(cartId));
        }

        [Test]
        public void ShouldThrowArgumentExceptionWithCorrectMessage_WhenEmptyCartIdIsPassed()
        {
            // Arrange
            var cartId = "";
            var expectedExMessage = "Shopping cart id cannot be null or empty.";
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                shoppingCartsService.GetShoppingCartById(cartId));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCartIdIsPassed()
        {
            // Arrange
            string cartId = null;
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                shoppingCartsService.GetShoppingCartById(cartId));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCartIdIsPassed()
        {
            // Arrange
            string cartId = null;
            var expectedExMessage = "Shopping cart id cannot be null or empty.";
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                shoppingCartsService.GetShoppingCartById(cartId));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallGetByIdMethodOfDataShoppingCartRepository()
        {
            // Arrange
            var cartId = "42";
            var shoppingCart = new ShoppingCart();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.GetById(It.IsAny<string>()))
                .Verifiable();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            shoppingCartsService.GetShoppingCartById(cartId);

            // Act and Assert
            mockedData.Verify(d =>
                d.ShoppingCartsRepository.GetById(cartId), Times.Once);
        }

        [Test]
        public void ShouldReturnShoppingCard()
        {
            // Arrange
            var cartId = "42";
            var shoppingCart = new ShoppingCart();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.GetById(It.IsAny<string>()))
                .Returns(shoppingCart)
                .Verifiable();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            var result = shoppingCartsService.GetShoppingCartById(cartId);

            // Act and Assert
            Assert.AreEqual(shoppingCart, result);
        }
    }
}
