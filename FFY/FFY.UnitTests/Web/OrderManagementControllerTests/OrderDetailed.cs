using FFY.Models;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models.OrderManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.OrderManagementControllerTests
{
    [TestFixture]
    public class OrderDetailed
    {
        [Test]
        public void ShouldCallGetOrderByIdMethodOfOrdersService()
        {
            // Arrange
            var id = 10;
            var orderViewModel = new OrderViewModel();
            var order = new Order();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.GetOrderById(It.IsAny<int>()))
                .Verifiable();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.OrderDetailed(orderViewModel, id);

            // Assert
            mockedOrdersService.Verify(os => os.GetOrderById(id), Times.Once);
        }

        [Test]
        public void ShouldAssignOrderToOrderViewModel_WhenOrderIsFound()
        {
            // Arrange
            var id = 10;
            var orderViewModel = new OrderViewModel();
            var order = new Order();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.GetOrderById(It.IsAny<int>()))
                .Returns(order);

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                             mockedOrdersService.Object);

            // Act 
            orderManagementController.OrderDetailed(orderViewModel, id);

            // Assert
            Assert.AreSame(order, orderViewModel.Order);
        }

        [Test]
        public void ShouldReturnPageNotFoundView_WhenOrderIsNotFound()
        {
            // Arrange
            var id = 10;
            var orderViewModel = new OrderViewModel();
            var order = new Order();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.GetOrderById(It.IsAny<int>()));

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                             mockedOrdersService.Object);

            // Act and Assert
            orderManagementController.WithCallTo(cmc => cmc.OrderDetailed(orderViewModel, id))
                .ShouldRenderView("PageNotFound");
        }

        [Test]
        public void ShouldReturnDefaultViewWithOrderViewModel_WhenOrderIsFound()
        {
            // Arrange
            var id = 10;
            var orderViewModel = new OrderViewModel();
            var order = new Order();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.GetOrderById(It.IsAny<int>()))
                .Returns(order);

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                             mockedOrdersService.Object);

            // Act and Assert
            orderManagementController.WithCallTo(cmc => cmc.OrderDetailed(orderViewModel, id))
                .ShouldRenderDefaultView()
                .WithModel<OrderViewModel>(model => Assert.AreEqual(orderViewModel, model));
        }
    }
}
