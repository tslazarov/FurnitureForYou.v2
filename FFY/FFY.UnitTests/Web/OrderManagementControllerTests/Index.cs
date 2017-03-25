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
    public class Index
    {
        [Test]
        public void ShouldReturnDefaultViewWithOrdersViewModel()
        {
            // Arrange
            var ordersViewModel = new OrdersViewModel();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedOrdersService = new Mock<IOrdersService>();

            var orderManagementController = new OrderManagementController(mockedMapperProvider.Object,
                   mockedOrdersService.Object);

            // Act and Assert
            orderManagementController.WithCallTo(cmc => cmc.Index(ordersViewModel))
                .ShouldRenderDefaultView()
                .WithModel<OrdersViewModel>(model => Assert.AreEqual(ordersViewModel, model));
        }
    }
}
