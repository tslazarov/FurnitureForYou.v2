using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FFY.UnitTests.Services.ProductsServiceTests
{
    [TestFixture]
    public class GetDiscountProducts
    {
        [Test]
        public void ShouldCallAllMethodOfData()
        {
            // Arrange
            var count = 10;

            var products = new List<Product>()
            {
                new Product() { Name = "Chair", DiscountPercentage = 2 },
                new Product() { Name = "Desk", DiscountPercentage = 10 },
                new Product() { Name = "Table", DiscountPercentage = 5 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable)
                .Verifiable();

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetDiscountProducts(count);

            // Assert
            mockedData.Verify(d => d.ProductsRepository.All(), Times.Once);
        }

        [Test]
        public void ShouldReturnAllProducts_OrderedByDiscountPercentage()
        {
            // Arrange
            var count = 10;

            var products = new List<Product>()
            {
                new Product() { Name = "Chair", DiscountPercentage = 2 },
                new Product() { Name = "Desk", DiscountPercentage = 10 },
                new Product() { Name = "Table", DiscountPercentage = 5 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetDiscountProducts(count);

            // Assert
            Assert.AreSame(products[1], result.First());
            Assert.AreSame(products[0], result.Last());
        }


        [Test]
        public void ShouldReturnACertainNumberOfProductsBasedOnCount()
        {
            // Arrange
            var count = 2;

            var products = new List<Product>()
            {
                new Product() { Name = "Chair", DiscountPercentage = 2 },
                new Product() { Name = "Bed", DiscountPercentage = 6 },
                new Product() { Name = "Table", DiscountPercentage = 5 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetDiscountProducts(count);

            // Assert
            Assert.AreEqual(count, result.Count());
        }
    }
}
