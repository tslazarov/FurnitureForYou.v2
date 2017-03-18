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
    public class Clear
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullShoppingCartIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var mockedProduct = new Mock<Product>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                shoppingCartsService.Clear(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullShoppingCartIsPassed()
        {
            // Arrange
            var expectedExMessage = "Shopping cart cannot be null.";
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var mockedProduct = new Mock<Product>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                shoppingCartsService.Clear(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldSetIsInCartPropertyOfCartProductsToFalse()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.CartProductsRepository.Update(It.IsAny<CartProduct>()));
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.Update(It.IsAny<ShoppingCart>()));
            mockedData.Setup(d => d.SaveChanges());
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var mockedProduct = new Mock<Product>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);
            var shoppingCart = new ShoppingCart()
            {
                CartProducts = new List<CartProduct>
                {
                    new CartProduct() { IsInCart = true },
                    new CartProduct() { IsInCart = true }
                }
            };

            // Act
            shoppingCartsService.Clear(shoppingCart);

            // Assert
            Assert.IsFalse(shoppingCart.CartProducts.Any(cp => cp.IsInCart));
        }

        [Test]
        public void ShouldCallUpdateMethodOfDataCartProductsRepositoryOnEveryProduct()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.CartProductsRepository.Update(It.IsAny<CartProduct>())).Verifiable();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.Update(It.IsAny<ShoppingCart>()));
            mockedData.Setup(d => d.SaveChanges());
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var mockedProduct = new Mock<Product>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);
            var shoppingCart = new ShoppingCart()
            {
                CartProducts = new List<CartProduct>
                {
                    new CartProduct() { IsInCart = true },
                    new CartProduct() { IsInCart = true }
                }
            };

            // Act
            shoppingCartsService.Clear(shoppingCart);

            // Assert
            mockedData.Verify(d =>
                d.CartProductsRepository.Update(It.IsAny<CartProduct>()), 
                Times.Exactly(shoppingCart.CartProducts.Count));
        }

        [Test]
        public void ShouldCallUpdateMethodOfDataShoppingCartsRepository()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.CartProductsRepository.Update(It.IsAny<CartProduct>()));
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.Update(It.IsAny<ShoppingCart>())).Verifiable();
            mockedData.Setup(d => d.SaveChanges());
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var mockedProduct = new Mock<Product>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);
            var shoppingCart = new ShoppingCart()
            {
                CartProducts = new List<CartProduct>
                {
                    new CartProduct() { IsInCart = true },
                    new CartProduct() { IsInCart = true }
                }
            };

            // Act
            shoppingCartsService.Clear(shoppingCart);

            // Assert
            mockedData.Verify(d =>
                d.ShoppingCartsRepository.Update(shoppingCart), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.CartProductsRepository.Update(It.IsAny<CartProduct>()));
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.Update(It.IsAny<ShoppingCart>()));
            mockedData.Setup(d => d.SaveChanges()).Verifiable();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var mockedProduct = new Mock<Product>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);
            var shoppingCart = new ShoppingCart()
            {
                CartProducts = new List<CartProduct>
                {
                    new CartProduct() { IsInCart = true },
                    new CartProduct() { IsInCart = true }
                }
            };

            // Act
            shoppingCartsService.Clear(shoppingCart);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
