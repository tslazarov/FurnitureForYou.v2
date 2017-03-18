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
    public class Remove
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
                shoppingCartsService.Remove(null, mockedProduct.Object));
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
                shoppingCartsService.Remove(null, mockedProduct.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullProductIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var mockedShoppingCart = new Mock<ShoppingCart>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                shoppingCartsService.Remove(mockedShoppingCart.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullProductIsPassed()
        {
            // Arrange
            var expectedExMessage = "Product cannot be null.";
            var mockedData = new Mock<IFFYData>();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            var mockedShoppingCart = new Mock<ShoppingCart>();

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                shoppingCartsService.Remove(mockedShoppingCart.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void ShouldRemoveCartProductFromShoppingCartCartProductsCollection_WhenProductWithIdIsFoundInTheShoppingCart(
            int id)
        {
            // Arrange
            var dummyProduct = new Product();
            var cartProduct = new CartProduct()
            {
                Product = dummyProduct,
                IsInCart = true
            };
            var cartProducts = new List<CartProduct>()
            {
                new CartProduct() { ProductId = 1, Product = dummyProduct, IsInCart = true },
                new CartProduct() { ProductId = 2, Product = dummyProduct, IsInCart = true },
                new CartProduct() { ProductId = 3, Product = dummyProduct, IsInCart = true }
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.Update(It.IsAny<ShoppingCart>()));
            mockedData.Setup(d => d.SaveChanges());
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            var product = new Product() { Id = id };
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartProducts = cartProducts;

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            var before = shoppingCart.CartProducts.Count;
            shoppingCartsService.Remove(shoppingCart, product);
            var after = shoppingCart.CartProducts.Count;

            // Assert
            Assert.AreEqual(before - 1, after);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void ShouldReCalculateTotalOfShoppingCart_WhenProductWithIdIsFoundInTheShoppingCart(
           int id)
        {
            // Arrange
            int quantity = 2;
            decimal discountedPrice = 10M;
            var dummyProduct = new Product()
            {
                DiscountedPrice = discountedPrice
            };
            var cartProduct = new CartProduct()
            {
                Product = dummyProduct,
                IsInCart = true
            };
            var cartProducts = new List<CartProduct>()
            {
                new CartProduct()
                {
                    ProductId = 1,
                    Product = dummyProduct,
                    Quantity = quantity,
                    IsInCart = true
                },
                new CartProduct() {
                    ProductId = 2,
                    Product = dummyProduct,
                    Quantity = 0,
                    IsInCart = true
                },
                new CartProduct()
                {
                    ProductId = 3,
                    Product = dummyProduct,
                    Quantity = quantity,
                    IsInCart = true
                },
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.Update(It.IsAny<ShoppingCart>()));
            mockedData.Setup(d => d.SaveChanges());
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            var product = new Product() { Id = id };
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartProducts = cartProducts;

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            shoppingCartsService.Remove(shoppingCart, product);

            // Assert
            // The cart contains three products, but one of them have quantity set to 0
            // The other one will be removed, so that leaves one product with quantity = 2 and price = 10
            Assert.AreEqual(quantity * discountedPrice, shoppingCart.Total);
        }

        [TestCase(1)]
        public void ShouldCallUpdateMethodOfDataShoppingCartsRepository_WhenProductWithIdIsFoundInTheShoppingCart(
            int id)
        {
            // Arrange
            decimal discountedPrice = 10M;
            var dummyProduct = new Product()
            {
                DiscountedPrice = discountedPrice
            };
            var cartProduct = new CartProduct()
            {
                Product = dummyProduct,
                IsInCart = true
            };
            var cartProducts = new List<CartProduct>()
            {
                new CartProduct() { ProductId = 1, Product = dummyProduct },
                new CartProduct() { ProductId = 2, Product = dummyProduct },
                new CartProduct() { ProductId = 3, Product = dummyProduct }
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.Update(It.IsAny<ShoppingCart>())).Verifiable();
            mockedData.Setup(d => d.SaveChanges());
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            var product = new Product() { Id = id };
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartProducts = cartProducts;

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            shoppingCartsService.Remove(shoppingCart, product);

            // Assert
            mockedData.Verify(d =>
                d.ShoppingCartsRepository.Update(shoppingCart), Times.Once);
        }

        [TestCase(1)]
        public void ShouldCallSaveChangesMethodOfData_WhenProductWithIdIsFoundInTheShoppingCart(
            int id)
        {
            // Arrange
            decimal discountedPrice = 10M;
            var dummyProduct = new Product()
            {
                DiscountedPrice = discountedPrice
            };
            var cartProduct = new CartProduct()
            {
                Product = dummyProduct,
                IsInCart = true
            };
            var cartProducts = new List<CartProduct>()
            {
                new CartProduct() { ProductId = 1, Product = dummyProduct },
                new CartProduct() { ProductId = 2, Product = dummyProduct },
                new CartProduct() { ProductId = 3, Product = dummyProduct }
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.Update(It.IsAny<ShoppingCart>()));
            mockedData.Setup(d => d.SaveChanges()).Verifiable();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();

            var product = new Product() { Id = id };
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartProducts = cartProducts;

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            shoppingCartsService.Remove(shoppingCart, product);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
