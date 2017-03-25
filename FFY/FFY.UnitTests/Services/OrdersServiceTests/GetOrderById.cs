using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;

namespace FFY.UnitTests.Services.OrdersServiceTests
{
    [TestFixture]
    public class GetOrderById
    {
        [TestCase(13)]
        [TestCase(42)]
        public void ShouldCallGetByIdMethodOfDataOrdersRepository(int id)
        {
            // Arrange
            var order = new Order() { Id = id };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.GetById(It.IsAny<int>()))
                .Returns(order)
                .Verifiable();

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            ordersService.GetOrderById(id);

            // Assert
            mockedData.Verify(d => d.OrdersRepository.GetById(id), Times.Once);
        }

        [TestCase(5)]
        [TestCase(7)]
        public void ShouldReturnCorrectCategory(int id)
        {
            // Arrange
            var order = new Order() { Id = id };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.GetById(It.IsAny<int>()))
                .Returns(order)
                .Verifiable();

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.GetOrderById(id);

            // Assert
            Assert.AreEqual(order, result);
        }
    }
}
