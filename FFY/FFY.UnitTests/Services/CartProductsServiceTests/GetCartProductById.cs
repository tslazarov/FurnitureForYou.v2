using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;

namespace FFY.UnitTests.Services.CartProductsServiceTests
{
    [TestFixture]
    class GetCartProductById
    {
        [TestCase(1)]
        [TestCase(4)]
        public void ShouldCallGetByIdMethodOfDataCartProductsRepository(int id)
        {
            // Arrange
            var cartProduct = new CartProduct() { Id = id };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.CartProductsRepository.GetById(It.IsAny<int>()))
                .Returns(cartProduct)
                .Verifiable();

            var cartProductsService = new CartProductsService(mockedData.Object);

            // Act
            cartProductsService.GetCartProductById(id);

            // Assert
            mockedData.Verify(d => d.CartProductsRepository.GetById(id), Times.Once);
        }

        [TestCase(1)]
        [TestCase(15)]
        public void ShouldReturnCorrectCartProduct(int id)
        {
            // Arrange
            var cartProduct = new CartProduct() { Id = id };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.CartProductsRepository.GetById(It.IsAny<int>()))
                .Returns(cartProduct)
                .Verifiable();

            var cartProductsService = new CartProductsService(mockedData.Object);

            // Act
            var result = cartProductsService.GetCartProductById(id);

            // Assert
            Assert.AreEqual(cartProduct, result);
        }
    }
}
