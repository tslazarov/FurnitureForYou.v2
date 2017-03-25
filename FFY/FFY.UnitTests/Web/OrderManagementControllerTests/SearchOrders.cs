using FFY.Models;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models;
using FFY.Web.Areas.Administration.Models.OrderManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.OrderManagementControllerTests
{
    [TestFixture]
    public class SearchOrders
    {
        [Test]
        public void ShouldCallSearchOrdersMethodOfOrdersService()
        {
            // Arrange
            var page = 1;
            var ordersPerPage = 10;

            var searchModel = new SearchModel();
            var ordersViewModel = new OrdersViewModel();

            var order = new Order();
            var orders = new List<Order>();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleOrderViewModel>>(It.IsAny<object>()));
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.SearchOrders(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(orders)
                .Verifiable();
            mockedOrdersService.Setup(os => os.GetOrdersCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(orders.Count);

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.SearchOrders(searchModel, ordersViewModel, page);

            // Assert
            mockedOrdersService.Verify(cs =>
                cs.SearchOrders(searchModel.SearchWord,
                    searchModel.SortBy,
                    searchModel.FilterBy,
                    page,
                    ordersPerPage), Times.Once);
        }

        [Test]
        public void ShouldCallGetOrdersCountMethodOfOrdersService()
        {
            // Arrange
            var searchModel = new SearchModel();
            var ordersViewModel = new OrdersViewModel();

            var order = new Order();
            var orders = new List<Order>();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleOrderViewModel>>(It.IsAny<object>()));
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.SearchOrders(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(orders);
            mockedOrdersService.Setup(os => os.GetOrdersCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(orders.Count)
                .Verifiable();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.SearchOrders(searchModel, ordersViewModel, null);

            // Assert
            mockedOrdersService.Verify(cs =>
                cs.GetOrdersCount(searchModel.SearchWord, searchModel.FilterBy), Times.Once);
        }

        [Test]
        public void ShouldSetSearchModelOfOrdersViewModel()
        {
            // Arrange
            var page = 1;

            var searchModel = new SearchModel();
            var ordersViewModel = new OrdersViewModel();

            var order = new Order();
            var orders = new List<Order>();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleOrderViewModel>>(It.IsAny<object>()));
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.SearchOrders(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(orders);
            mockedOrdersService.Setup(os => os.GetOrdersCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(orders.Count);

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.SearchOrders(searchModel, ordersViewModel, page);

            // Assert
            Assert.AreSame(searchModel, ordersViewModel.SearchModel);
        }

        [Test]
        public void ShouldSetOrdersCountOfOrdersViewModel()
        {
            // Arrange
            var page = 1;

            var searchModel = new SearchModel();
            var ordersViewModel = new OrdersViewModel();

            var order = new Order();
            var orders = new List<Order>() { order };

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleOrderViewModel>>(It.IsAny<object>()));
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.SearchOrders(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(orders);
            mockedOrdersService.Setup(os => os.GetOrdersCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(orders.Count)
                .Verifiable();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.SearchOrders(searchModel, ordersViewModel, page);

            // Assert
            Assert.AreEqual(orders.Count, ordersViewModel.OrdersCount);
        }

        [Test]
        public void ShouldSetPagesOfOrdersViewModel()
        {
            // Arrange
            var ordersPerPage = 10;
            var page = 1;

            var searchModel = new SearchModel();
            var ordersViewModel = new OrdersViewModel();

            var order = new Order();
            var orders = new List<Order>() { order };

            var expectedPages = (int)Math.Ceiling((double)orders.Count / ordersPerPage);

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleOrderViewModel>>(It.IsAny<object>()));
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.SearchOrders(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(orders);
            mockedOrdersService.Setup(os => os.GetOrdersCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(orders.Count)
                .Verifiable();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);
            
            // Act 
            orderManagementController.SearchOrders(searchModel, ordersViewModel, page);

            // Assert
            Assert.AreEqual(expectedPages, ordersViewModel.Pages);
        }

        [Test]
        public void ShouldSetPageOfOrdersViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var ordersViewModel = new OrdersViewModel();

            var order = new Order();
            var orders = new List<Order>() { order };

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleOrderViewModel>>(It.IsAny<object>()));
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.SearchOrders(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(orders);
            mockedOrdersService.Setup(os => os.GetOrdersCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(orders.Count)
                .Verifiable();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.SearchOrders(searchModel, ordersViewModel, page);

            // Assert
            Assert.AreEqual(page, ordersViewModel.Page);
        }

        [Test]
        public void ShouldMapAndSetOrdersOfOrdersViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var ordersViewModel = new OrdersViewModel();

            var order = new Order();
            var orders = new List<Order>() { order };

            var singleOrders = new List<SingleOrderViewModel>();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp =>
                mp.Map<IEnumerable<SingleOrderViewModel>>(It.IsAny<object>()))
                .Returns(singleOrders);
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.SearchOrders(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(orders);
            mockedOrdersService.Setup(os => os.GetOrdersCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(orders.Count)
                .Verifiable();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.SearchOrders(searchModel, ordersViewModel, page);

            // Assert
            CollectionAssert.AreEquivalent(singleOrders, ordersViewModel.Orders);
        }

        [Test]
        public void ShouldRenderOrdersPartialViewWithOrdersViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var ordersViewModel = new OrdersViewModel();

            var order = new Order();
            var orders = new List<Order>() { order };

            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleOrderViewModel>>(It.IsAny<object>()));
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.SearchOrders(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(orders);
            mockedOrdersService.Setup(os => os.GetOrdersCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(orders.Count)
                .Verifiable();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act and Assert
            orderManagementController.WithCallTo(omc => omc.SearchOrders(searchModel,
                ordersViewModel,
                page))
                .ShouldRenderPartialView("OrdersPartial")
                .WithModel<OrdersViewModel>(model => Assert.AreEqual(ordersViewModel, model));
        }
    }
}
