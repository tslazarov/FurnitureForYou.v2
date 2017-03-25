using FFY.Data.Contracts;
using FFY.Data.Factories;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FFY.UnitTests.Services.ShoppingCartsServiceTests
{
    [TestFixture]
    public class CartProductsCount
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
                shoppingCartsService.CartProductsCount(cartId));
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
                shoppingCartsService.CartProductsCount(cartId));
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
                shoppingCartsService.CartProductsCount(cartId));
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
                shoppingCartsService.CartProductsCount(cartId));
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
                .Returns(shoppingCart)
                .Verifiable();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            shoppingCartsService.CartProductsCount(cartId);

            // Act and Assert
            mockedData.Verify(d =>
                d.ShoppingCartsRepository.GetById(cartId), Times.Once);
        }

        [Test]
        public void ShouldReturnCorrectCountOfCartProductsWithPropertyIsInCartSetToTrue()
        {
            // Arrange
            var cartId = "42";
            var shoppingCart = new ShoppingCart()
            {
                CartProducts = new List<CartProduct>
                {
                    new CartProduct() { IsInCart = true },
                    new CartProduct() { IsInCart = true },
                    new CartProduct() { IsInCart = false },
                }
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.GetById(It.IsAny<string>()))
                .Returns(shoppingCart);
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            var result = shoppingCartsService.CartProductsCount(cartId);

            // Assert
            Assert.AreEqual(2, result);
        }
    }
}
