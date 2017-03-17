using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Services.ProductsServiceTests
{
    [TestFixture]
    public class GetProductById
    {
        [TestCase(1)]
        [TestCase(2)]
        public void ShouldCallGetByIdMethodOfDataProductsRepository(int id)
        {
            // Arrange
            var product = new Product() { Id = id };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.GetById(It.IsAny<int>()))
                .Returns(product)
                .Verifiable();

            var productsService = new ProductsService(mockedData.Object);

            // Act
            productsService.GetProductById(id);

            // Assert
            mockedData.Verify(d => d.ProductsRepository.GetById(id), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void ShouldReturnCorrectProduct(int id)
        {
            // Arrange
            var product = new Product() { Id = id };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.GetById(It.IsAny<int>()))
                .Returns(product)
                .Verifiable();

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductById(id);

            // Assert
            Assert.AreEqual(product, result);
        }
    }
}
