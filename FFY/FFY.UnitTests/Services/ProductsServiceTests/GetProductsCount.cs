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
    public class GetProductsCount
    {
        [Test]
        public void ShouldReturnAllProductsCount_WhenNoSearchWordIsProvided()
        {
            // Arrange
            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsCount(null);

            // Assert
            Assert.AreEqual(products.Count, result);
        }

        [TestCase("bed", 1)]
        [TestCase("e", 3)]
        [TestCase("x", 0)]
        public void ShouldReturnCorrectProductsCount_WhenSearchWordIsProvided(string searchWord,
            int expectedCount)
        {
            // Arrange
            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" },
                new Product() { Name = "Table" },
                new Product() { Name = "Wardrobe" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsCount(searchWord);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
