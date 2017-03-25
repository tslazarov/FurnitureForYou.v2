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
    public class UpdateStatus
    {
       [Test]
        public void ShouldCallGetOrderByIdMethodOfOrdersService()
        {
            // Arrange
            var id = 2;
            var orderViewModel = new OrderViewModel()
            {
                Order = new Order() { Id = id }
            };
            var order = new Order();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.GetOrderById(It.IsAny<int>()))
                .Returns(order)
                .Verifiable();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.UpdateStatus(orderViewModel);

            // Assert
            mockedOrdersService.Verify(os => os.GetOrderById(id), Times.Once);
        }

        [Test]
        public void ShouldCallUpdateOrderStatusMethodOfOrdersService()
        {
            // Arrange
            var id = 2;
            var orderViewModel = new OrderViewModel()
            {
                Order = new Order() { Id = id }
            };
            var order = new Order();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(cs => cs.GetOrderById(It.IsAny<int>()))
                .Returns(order);
            mockedOrdersService.Setup(os => os.UpdateOrderStatuses(It.IsAny<Order>(),
                It.IsAny<OrderStatusType>(),
                It.IsAny<OrderPaymentStatusType>()))
                .Verifiable();


            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.UpdateStatus(orderViewModel);

            // Assert
            mockedOrdersService.Verify(os =>
                os.UpdateOrderStatuses(order, 
                    It.IsAny<OrderStatusType>(), 
                    It.IsAny<OrderPaymentStatusType>()), 
                Times.Once);
        }

        [Test]
        public void ShouldRedirectToOrderDetailed()
        {
            // Arrange
            var id = 2;
            var orderViewModel = new OrderViewModel()
            {
                Order = new Order() { Id = id }
            };
            var order = new Order();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();
            mockedOrdersService.Setup(os => os.GetOrderById(It.IsAny<int>()))
                .Returns(order)
                .Verifiable();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act 
            orderManagementController.UpdateStatus(orderViewModel);

            // Act and Assert
            orderManagementController.WithCallTo(cmc => cmc.UpdateStatus(orderViewModel))
                .ShouldRedirectTo((OrderManagementController om) =>
                    om.OrderDetailed(orderViewModel, orderViewModel.Order.Id));
        }
    }
}
