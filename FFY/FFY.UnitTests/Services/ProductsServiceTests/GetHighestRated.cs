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
    public class GetHighestRated
    {
        [Test]
        public void ShouldCallAllMethodOfData()
        {
            // Arrange
            var count = 10;

            var products = new List<Product>()
            {
                new Product() { Name = "Bed", Rating = 4 },
                new Product() { Name = "Wardrobe", Rating = 2 },
                new Product() { Name = "Table", Rating = 1 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable)
                .Verifiable();

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetHighestRatedProducts(count);

            // Assert
            mockedData.Verify(d => d.ProductsRepository.All(), Times.Once);
        }

        [Test]
        public void ShouldReturnAllProducts_OrderedByRating()
        {
            // Arrange
            var count = 10;

            var products = new List<Product>()
            {
                new Product() { Name = "Bed", Rating = 3 },
                new Product() { Name = "Wardrobe", Rating = 2 },
                new Product() { Name = "Table", Rating = 5 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetHighestRatedProducts(count);

            // Assert
            Assert.AreSame(products[2], result.First());
            Assert.AreSame(products[1], result.Last());
        }


        [Test]
        public void ShouldReturnACertainNumberOfProductsBasedOnCount()
        {
            // Arrange
            var count = 2;

            var products = new List<Product>()
            {
                new Product() { Name = "Bed", Rating = 4 },
                new Product() { Name = "Wardrobe", Rating = 2 },
                new Product() { Name = "Table", Rating = 1 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetLatestProducts(count);

            // Assert
            Assert.AreEqual(count, result.Count());
        }
    }
}
