﻿using FFY.Data.Contracts;
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
    public class Add
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
                shoppingCartsService.Add(null, mockedProduct.Object, 1));
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
                shoppingCartsService.Add(null, mockedProduct.Object, 1));
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
                shoppingCartsService.Add(mockedShoppingCart.Object, null, 1));
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
                shoppingCartsService.Add(mockedShoppingCart.Object, null, 1));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [TestCase(-5)]
        [TestCase(42)]
        public void ShouldCallCreateCartProductMethodOfCartProductFactory_WhenProductWithIdIsNotFoundInTheShoppingCart(
            int id)
        {
            // Arrange
            int quantity = 2;
            var dummyProduct = new Product();
            var cartProduct = new CartProduct() {
                Product = dummyProduct,
                IsInCart = true,
                IsOutOfStock = false
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
            mockedData.Setup(d => d.SaveChanges());
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            mockedCartProductFactory.Setup(cpf =>
                cpf.CreateCartProduct(It.IsAny<int>(),
                    It.IsAny<Product>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()
                ))
                .Returns(cartProduct)
                .Verifiable();
            var product = new Product();
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartProducts = cartProducts;

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            shoppingCartsService.Add(shoppingCart, product, quantity);

            // Assert
            mockedCartProductFactory.Verify(cpf =>
                cpf.CreateCartProduct(quantity, product, true, false), Times.Once);
        }

        [TestCase(-5)]
        [TestCase(42)]
        public void ShouldAddCartProductToShoppingCartCartProductsCollection_WhenProductWithIdIsNotFoundInTheShoppingCart(
            int id)
        {
            // Arrange
            int quantity = 2;
            var dummyProduct = new Product();
            var cartProduct = new CartProduct()
            {
                Product = dummyProduct,
                IsInCart = true,
                IsOutOfStock = false
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
            mockedData.Setup(d => d.SaveChanges());
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            mockedCartProductFactory.Setup(cpf =>
                cpf.CreateCartProduct(It.IsAny<int>(),
                    It.IsAny<Product>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()
                ))
                .Returns(cartProduct);
            var product = new Product();
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartProducts = cartProducts;

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            var before = shoppingCart.CartProducts.Count;
            shoppingCartsService.Add(shoppingCart, product, quantity);
            var after = shoppingCart.CartProducts.Count;
            // Assert
            Assert.AreEqual(before + 1, after);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void ShouldCalculateTotalOfShoppingCart_WhenProductWithIdIsFoundInTheShoppingCart(
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
            mockedCartProductFactory.Setup(cpf =>
                cpf.CreateCartProduct(It.IsAny<int>(),
                    It.IsAny<Product>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()
                ))
                .Returns(cartProduct);
            var product = new Product() { Id = id };
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartProducts = cartProducts;

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            shoppingCartsService.Add(shoppingCart, product, quantity);

            // Assert
            // The cart contains three products, but one of them have quantity set to 0
            // The other three products have same price and quantity - 1x4q and 1x2q
            Assert.AreEqual(3 * quantity * discountedPrice, shoppingCart.Total);
        }

        [TestCase(1)]
        public void ShouldCallUpdateMethodOfDataShoppingCartsRepository_WhenProductWithIdIsFoundInTheShoppingCart(
            int id)
        {
            // Arrange
            int quantity = 2;
            decimal discountedPrice = 10M;
            var dummyProduct = new Product()
            {
                DiscountedPrice = discountedPrice,
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
            mockedCartProductFactory.Setup(cpf =>
                cpf.CreateCartProduct(It.IsAny<int>(),
                    It.IsAny<Product>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()
                ))
                .Returns(cartProduct);
            var product = new Product() { Id = id };
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartProducts = cartProducts;

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            shoppingCartsService.Add(shoppingCart, product, quantity);

            // Assert
            mockedData.Verify(d => 
                d.ShoppingCartsRepository.Update(shoppingCart), Times.Once);
        }

        [TestCase(1)]
        public void ShouldCallSaveChangesMethodOfData_WhenProductWithIdIsFoundInTheShoppingCart(
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
                new CartProduct() { ProductId = 1, Product = dummyProduct },
                new CartProduct() { ProductId = 2, Product = dummyProduct },
                new CartProduct() { ProductId = 3, Product = dummyProduct }
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d =>
                d.ShoppingCartsRepository.Update(It.IsAny<ShoppingCart>()));
            mockedData.Setup(d => d.SaveChanges()).Verifiable();
            var mockedCartProductFactory = new Mock<ICartProductFactory>();
            mockedCartProductFactory.Setup(cpf =>
                cpf.CreateCartProduct(It.IsAny<int>(),
                    It.IsAny<Product>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()
                ))
                .Returns(cartProduct);
            var product = new Product() { Id = id };
            var shoppingCart = new ShoppingCart();
            shoppingCart.CartProducts = cartProducts;

            var shoppingCartsService = new ShoppingCartsService(mockedData.Object,
                mockedCartProductFactory.Object);

            // Act
            shoppingCartsService.Add(shoppingCart, product, quantity);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
