using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.OrdersServiceTests
{
    [TestFixture]
    public class UpdateOrderStatuses
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullOrderIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var ordersService = new OrdersService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                ordersService.UpdateOrderStatuses(null,
                    OrderStatusType.Processing,
                    OrderPaymentStatusType.Payed));
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
                ordersService.UpdateOrderStatuses(null,
                    OrderStatusType.Processing,
                    OrderPaymentStatusType.Payed));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldAssignNewOrderStatusAndOrderPaymentStatusOnOrder()
        {
            // Arrange
            var orderStatus = OrderStatusType.Delivered;
            var orderPaymentStatus = OrderPaymentStatusType.Payed;

            var order = new Order() {
                OrderStatusType = OrderStatusType.Processing,
                OrderPaymentStatusType = OrderPaymentStatusType.PaymentOnDelivery
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.Update(It.IsAny<Order>()));
            mockedData.Setup(d => d.SaveChanges());

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            ordersService.UpdateOrderStatuses(order, orderStatus, orderPaymentStatus);

            // Assert
            Assert.AreEqual(orderStatus, order.OrderStatusType);
            Assert.AreEqual(orderPaymentStatus, order.OrderPaymentStatusType);
        }

        [Test]
        public void ShouldCallUpdateMethodOfDataOrdersRepository()
        {
            // Arrange
            var orderStatus = OrderStatusType.Delivered;
            var orderPaymentStatus = OrderPaymentStatusType.Payed;

            var order = new Order()
            {
                OrderStatusType = OrderStatusType.Processing,
                OrderPaymentStatusType = OrderPaymentStatusType.PaymentOnDelivery
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.Update(It.IsAny<Order>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges());

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            ordersService.UpdateOrderStatuses(order, orderStatus, orderPaymentStatus);

            // Assert
            mockedData.Verify(d => d.OrdersRepository.Update(order), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var orderStatus = OrderStatusType.Delivered;
            var orderPaymentStatus = OrderPaymentStatusType.Payed;

            var order = new Order()
            {
                OrderStatusType = OrderStatusType.Processing,
                OrderPaymentStatusType = OrderPaymentStatusType.PaymentOnDelivery
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.Update(It.IsAny<Order>()));
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            ordersService.UpdateOrderStatuses(order, orderStatus, orderPaymentStatus);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
