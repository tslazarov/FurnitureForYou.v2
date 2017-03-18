using FFY.Data.Contracts;
using FFY.Data.Factories;
using FFY.Models;
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
    public class CartProductsCount
    {
        [TestCase("")]
        [TestCase(null)]
        public void ShouldThrowArgumentException_WhenNullOrEmptyCartIdIsPassed(string cartId)
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                shoppingCartsService.CartProductsCount(cartId));
        }

        [TestCase("")]
        [TestCase(null)]
        public void ShouldThrowArgumentExceptionWithCorrectMessage_WhenNullOrEmptyCartIdIsPassed(string cartId)
        {
            // Arrange
            var expectedExMessage = "Shopping cart id cannot be null.";
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
        public void ShouldCallGetByIdMethodOfDataShoppingCartRepository()
        {
            // Arrange
            var cartId = "42";
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.GetById(It.IsAny<string>()))
                .Returns(new ShoppingCart())
                .Verifiable();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

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
