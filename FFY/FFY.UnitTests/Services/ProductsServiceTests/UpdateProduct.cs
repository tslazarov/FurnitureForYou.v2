using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.ProductsServiceTests
{
    [TestFixture]
    public class UpdateProduct
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullProductIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var productsService = new ProductsService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => productsService.UpdateProduct(null));
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
                productsService.UpdateProduct(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallUpdateMethodOfDataContactRepository()
        {
            // Arrange
            var mockedProduct = new Mock<Product>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()))
                .Verifiable();

            var productsService = new ProductsService(mockedData.Object);

            // Act
            productsService.UpdateProduct(mockedProduct.Object);

            // Assert
            mockedData.Verify(d =>
                d.ProductsRepository.Update(mockedProduct.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedProduct = new Mock<Product>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.Update(It.IsAny<Product>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var productsService = new ProductsService(mockedData.Object);

            // Act
            productsService.UpdateProduct(mockedProduct.Object);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
