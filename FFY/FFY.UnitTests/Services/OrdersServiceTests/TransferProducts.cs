using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFY.UnitTests.Services.OrdersServiceTests
{
    [TestFixture]
    public class TransferProducts
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullOrderIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var mockedShoppingCart = new Mock<ShoppingCart>();

            var ordersService = new OrdersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => 
                ordersService.TransferProducts(null, mockedShoppingCart.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullOrderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Order cannot be null.";

            var mockedData = new Mock<IFFYData>();
            var mockedShoppingCart = new Mock<ShoppingCart>();

            var ordersService = new OrdersService(mockedData.Object);


            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                ordersService.TransferProducts(null, mockedShoppingCart.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullShoppingCartIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var mockedOrder = new Mock<Order>();

            var ordersService = new OrdersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                ordersService.TransferProducts(mockedOrder.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullShoppingCartIsPassed()
        {
            // Arrange
            var expectedExMessage = "Shopping cart cannot be null.";

            var mockedData = new Mock<IFFYData>();
            var mockedOrder = new Mock<Order>();

            var ordersService = new OrdersService(mockedData.Object);


            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                ordersService.TransferProducts(mockedOrder.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldAssignCorrectProductsToOrderProductsCollection()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();
            var order = new Order();
            var shoppingCart = new ShoppingCart()
            {
                CartProducts = new List<CartProduct>()
                {
                    new CartProduct() { IsInCart = true, Product = new Product() },
                    new CartProduct() { IsInCart = true, Product = new Product() },
                    new CartProduct() { IsInCart = false, Product = new Product() },
                }
            };

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            ordersService.TransferProducts(order, shoppingCart);

            // Assert
            Assert.AreEqual(2, order.Products.Count);
        }

        [TestCase(5, 2)]
        [TestCase(10, 4)]
        public void ShouldReduceQuantityOfProductBasedOnCartProductQuantity(int productQuantity, int cartProductQuantity)
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var product = new Product()
            {
                Quantity = productQuantity
            };

            var cartProduct = new CartProduct()
            {
                Quantity = cartProductQuantity,
                IsInCart = true,
                Product = product
            };

            var order = new Order();
            var shoppingCart = new ShoppingCart()
            {
                CartProducts = new List<CartProduct>()
                {
                    cartProduct
                }
            };

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            ordersService.TransferProducts(order, shoppingCart);

            // Assert
            Assert.AreEqual(productQuantity - cartProductQuantity, order.Products.First().Product.Quantity);
        }

        [TestCase(2, 5)]
        [TestCase(4, 10)]
        public void ShouldSetIsOutOfStock_WhenProductQuantityBecomesNegative(int productQuantity, int cartProductQuantity)
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var product = new Product()
            {
                Quantity = productQuantity
            };

            var cartProduct = new CartProduct()
            {
                Quantity = cartProductQuantity,
                IsInCart = true,
                IsOutOfStock = false,
                Product = product
            };

            var order = new Order();
            var shoppingCart = new ShoppingCart()
            {
                CartProducts = new List<CartProduct>()
                {
                    cartProduct
                }
            };

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            ordersService.TransferProducts(order, shoppingCart);

            // Assert
            Assert.IsTrue(order.Products.First().IsOutOfStock);
        }
    }
}
