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
    public class SearchOrders
    {
        [Test]
        public void ShouldReturnAllOrders_WhenNoSearchWordAndFilterAreProvided()
        {
            // Arrange
            var orders = new List<Order>()
            {
                new Order() { User = new User() { FirstName = "Frank" } },
                new Order() { User = new User() { FirstName = "George" } }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.SearchOrders(null, "sender", null, 1, 10);

            // Assert
            CollectionAssert.AreEquivalent(orders, result);
        }

        [Test]
        public void ShouldReturnCorrectOrders_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "george";

            var orders = new List<Order>()
            {
                new Order() {
                    User = new User() { FirstName = "Frank" },
                    Address = new Address() { Street = "Street" }
                },
                new Order() {
                    User = new User() { FirstName = "George" },
                    Address = new Address() { Street = "Street" }
                }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.SearchOrders(searchWord, "sender", "", 1, 10);

            // Assert
            Assert.AreSame(orders[1], result.First());
        }

        [Test]
        public void ShouldReturnCorrectOrdersCollection_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "street";

            var orders = new List<Order>()
            {
                new Order() {
                    User = new User() { FirstName = "Frank" },
                    Address = new Address() { Street = "Street" }
                },
                new Order() {
                    User = new User() { FirstName = "Patrick" },
                    Address = new Address() { Street = "Something else" }
                },
                new Order() {
                    User = new User() { FirstName = "George" },
                    Address = new Address() { Street = "Street" }
                }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.SearchOrders(searchWord, "sender", "", 1, 10);

            // Assert
            Assert.AreSame(orders[0], result.First());
            Assert.AreSame(orders[2], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectOrdersSortedBySender_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "street";

            var orders = new List<Order>()
            {
                new Order() {
                    User = new User() { FirstName = "Patrick" },
                    Address = new Address() { Street = "Street" }
                },
                new Order() {
                    User = new User() { FirstName = "Abraham" },
                    Address = new Address() { Street = "Street 1" }
                },
                new Order() {
                    User = new User() { FirstName = "Neil" },
                    Address = new Address() { Street = "Street 2" }
                }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.SearchOrders(searchWord, "sender", "", 1, 10);

            // Assert
            Assert.AreSame(orders[1], result.First());
            Assert.AreSame(orders[0], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectOrdersSortedByAddress_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "street";

            var orders = new List<Order>()
            {
                new Order() {
                    User = new User() { FirstName = "Patrick" },
                    Address = new Address() { Street = "Street Z" }
                },
                new Order() {
                    User = new User() { FirstName = "Abraham" },
                    Address = new Address() { Street = "Street A" }
                },
                new Order() {
                    User = new User() { FirstName = "Neil" },
                    Address = new Address() { Street = "Street Y" }
                }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.SearchOrders(searchWord, "address", "", 1, 10);

            // Assert
            Assert.AreSame(orders[1], result.First());
            Assert.AreSame(orders[0], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectOrdersSortedByDate_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "street";

            var orders = new List<Order>()
            {
                new Order() {
                    User = new User() { FirstName = "Patrick" },
                    Address = new Address() { Street = "Street 1" },
                    SendOn = new DateTime(2017, 1, 1)
                },
                new Order() {
                    User = new User() { FirstName = "Abraham" },
                    Address = new Address() { Street = "Street 2" },
                    SendOn = new DateTime(2017, 1, 3)
                },
                new Order() {
                    User = new User() { FirstName = "Neil" },
                    Address = new Address() { Street = "Street 3" },
                    SendOn = new DateTime(2016, 12, 2)

                }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.SearchOrders(searchWord, "date", "", 1, 10);

            // Assert
            Assert.AreSame(orders[1], result.First());
            Assert.AreSame(orders[2], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectOrdersPerPage_WhenSearchWordAndPageAreProvided()
        {
            // Arrange
            var searchWord = "street";

            var orders = new List<Order>()
            {
                new Order() {
                    User = new User() { FirstName = "Elon" },
                    Address = new Address() { Street = "Street -1" }
                },
                new Order() {
                    User = new User() { FirstName = "Noah" },
                    Address = new Address() { Street = "Street 1" }
                },
                new Order() {
                    User = new User() { FirstName = "Samuel" },
                    Address = new Address() { Street = "Street 2" }
                }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.SearchOrders(searchWord, "sender", "", 3, 1);

            // Assert
            Assert.AreSame(orders[2], result.First());
        }

        [Test]
        public void ShouldReturnNoOrders_WhenNoContactsAreMatchingTheCriterias()
        {
            // Arrange
            var searchWord = "qwerty";

            var orders = new List<Order>()
            {
                new Order() {
                    User = new User() { FirstName = "Elon" },
                    Address = new Address() { Street = "Street -1" }
                },
                new Order() {
                    User = new User() { FirstName = "Noah" },
                    Address = new Address() { Street = "Street 1" }
                },
                new Order() {
                    User = new User() { FirstName = "Samuel" },
                    Address = new Address() { Street = "Street 2" }
                }
            };
            
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.OrdersRepository.All())
                .Returns(orders.AsQueryable);

            var ordersService = new OrdersService(mockedData.Object);

            // Act
            var result = ordersService.SearchOrders(searchWord, "sender", "", 1, 10);

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
