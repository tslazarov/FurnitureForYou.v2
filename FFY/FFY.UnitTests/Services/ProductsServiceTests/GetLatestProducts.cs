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
    public class GetLatestProducts
    {
        [Test]
        public void ShouldCallAllMethodOfData()
        {
            // Arrange
            var count = 10;

            var products = new List<Product>()
            {
                new Product() { Name = "Bed", Id = 4 },
                new Product() { Name = "Chair", Id = 6 },
                new Product() { Name = "Sofa", Id = 10 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable)
                .Verifiable();

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetLatestProducts(count);

            // Assert
            mockedData.Verify(d => d.ProductsRepository.All(), Times.Once);
        }

        [Test]
        public void ShouldReturnAllProducts_OrderedById()
        {
            // Arrange
            var count = 10;

            var products = new List<Product>()
            {
                new Product() { Name = "Bed", Id = 4 },
                new Product() { Name = "Chair", Id = 6 },
                new Product() { Name = "Sofa", Id = 10 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetLatestProducts(count);

            // Assert
            Assert.AreSame(products[2], result.First());
            Assert.AreSame(products[0], result.Last());
        }


        [Test]
        public void ShouldReturnACertainNumberOfProductsBasedOnCount()
        {
            // Arrange
            var count = 2;

            var products = new List<Product>()
            {
                new Product() { Name = "Bed", Id = 4 },
                new Product() { Name = "Chair", Id = 6 },
                new Product() { Name = "Sofa", Id = 10 }
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
