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
    public class AddProduct
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullProductIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var productsService = new ProductsService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => productsService.AddProduct(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullProductIsPassed()
        {
            // Arrange
            var expectedExMessage = "Product cannot be null.";

            var mockedData = new Mock<IFFYData>();

            var productsService = new ProductsService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                productsService.AddProduct(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallAddMethodOfDataContactRepository()
        {
            // Arrange
            var mockedProduct = new Mock<Product>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.Add(It.IsAny<Product>()))
                .Verifiable();

            var productsService = new ProductsService(mockedData.Object);

            // Act
            productsService.AddProduct(mockedProduct.Object);

            // Assert
            mockedData.Verify(d =>
                d.ProductsRepository.Add(mockedProduct.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedProduct = new Mock<Product>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.Add(It.IsAny<Product>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var productsService = new ProductsService(mockedData.Object);

            // Act
            productsService.AddProduct(mockedProduct.Object);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
