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

namespace FFY.UnitTests.Services.OrdersServiceTests
{
    [TestFixture]
    public class GetOrdersCount
    {
        [Test]
        public void ShouldReturnAllOrdersCount_WhenNoSearchWordAndFilterAreProvided()
        {
            // Arrange
            var orders = new List<Order>()
            {
                new Order(),
                new Order()
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.GetOrdersCount(null, null);

            // Assert
            Assert.AreEqual(orders.Count, result);
        }

        [TestCase("elon", 1)]
        [TestCase("street", 2)]
        [TestCase("xyz", 0)]
        public void ShouldReturnOrdersOrdersCount_WhenSearchWordIsProvided(string searchWord,
            int expectedCount)
        {
            // Arrange
            var orders = new List<Order>()
            {
                new Order() {
                    User = new User() { FirstName = "Elon" },
                    Address = new Address() { Street = "Street" }
                },
                new Order() {
                    User = new User() { FirstName = "Eugene" },
                    Address = new Address() { Street = "Street 2" }
                },
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.GetOrdersCount(searchWord, null);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestCase("1", 2)]
        [TestCase("0", 4)]
        [TestCase("3", 1)]
        [TestCase("4", 1)]
        public void ShouldReturnCorrectOrdersCount_WhenFilterIsProvided(string filterBy,
            int expectedCount)
        {
            // Arrange
            var orders = new List<Order>()
            {
                new Order() { OrderStatusType = OrderStatusType.Delivered },
                new Order() { OrderStatusType = OrderStatusType.Processing },
                new Order() { OrderStatusType = OrderStatusType.Processing },
                new Order() { OrderStatusType = OrderStatusType.Rejected },
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.GetOrdersCount(null, filterBy);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestCase("street", "1", 2)]
        [TestCase("elon", "1", 1)]
        [TestCase("n", "0", 3)]
        [TestCase("street", "2", 0)]
        public void ShouldReturnCorrectOrdersCount_WhenSearchWordAndFilterAreProvided(string searchWord,
            string filterBy,
            int expectedCount)
        {
            // Arrange
            var orders = new List<Order>()
            {
                new Order() {
                    User = new User() { FirstName = "Elon" },
                    Address = new Address() { Street = "Street" },
                    OrderStatusType = OrderStatusType.Processing
                },
                new Order() {
                    User = new User() { FirstName = "Eugene" },
                    Address = new Address() { Street = "Street 2" },
                    OrderStatusType = OrderStatusType.Processing
                },
                new Order() {
                    User = new User() { FirstName = "Frank" },
                    Address = new Address() { Street = "Street 3" },
                    OrderStatusType = OrderStatusType.Delivered
                }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.GetOrdersCount(searchWord, filterBy);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
