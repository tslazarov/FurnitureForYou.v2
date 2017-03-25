using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.OrdersServiceTests
{
    [TestFixture]
    public class AddOrder
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullOrderIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var ordersService = new OrdersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => ordersService.AddOrder(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullOrderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Order cannot be null.";

            var mockedData = new Mock<IFFYData>();

            var ordersService = new OrdersService(mockedData.Object);


            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                ordersService.AddOrder(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallAddMethodOfDataOrdersRepository()
        {
            // Arrange
            var mockedOrder = new Mock<Order>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.Add(It.IsAny<Order>()))
                .Verifiable();

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            ordersService.AddOrder(mockedOrder.Object);

            // Assert
            mockedData.Verify(d =>
                d.OrdersRepository.Add(mockedOrder.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedOrder = new Mock<Order>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.Add(It.IsAny<Order>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            ordersService.AddOrder(mockedOrder.Object);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
