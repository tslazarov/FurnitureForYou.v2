using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Services.Utilities;
using FFY.UnitTests.Web.ProductManagementControllerTests.Mocks;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models.ProductManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFY.UnitTests.Web.ProductManagementControllerTests
{
    [TestFixture]
    public class AddProduct
    {
        [Test]
        public void ShouldCallUploadMethodOfImageUploader()
        {
            // Arrange
            var imageFileName = "default-product-image.jpg";
            var folderName = "products";

            var productsOperationViewModel = new ProductOperationViewModel();

            var mockedRequestProvider = new Mock<IHttpRequestProvider>();
            mockedRequestProvider.SetupGet(rp => rp.RequestFiles)
                .Returns(new MockedHttpFileCollectionBase());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedImageUploader = new Mock<IImageUploader>();
            mockedImageUploader.Setup(iu => iu.Upload(It.IsAny<HttpPostedFileBase>(),
                It.IsAny<HttpServerUtilityBase>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Verifiable();
            var mockedProductFactory = new Mock<IProductFactory>();
            var mockedProductsService = new Mock<IProductsService>();
            var mockedRoomFactory = new Mock<IRoomFactory>();
            var mockedRoomsService = new Mock<IRoomsService>();
            mockedRoomsService.Setup(rs => rs.GetRoomById(It.IsAny<int>()))
                .Returns(new Room());
            var mockedCategoryFactory = new Mock<ICategoryFactory>();
            var mockedCategoriesService = new Mock<ICategoriesService>();
            mockedCategoriesService.Setup(cs => cs.GetCategoryById(It.IsAny<int>()))
                .Returns(new Category());

            var productManagementController = new ProductManagementController(mockedRequestProvider.Object,
                    mockedMapperProvider.Object,
                    mockedImageUploader.Object,
                    mockedProductFactory.Object,
                    mockedProductsService.Object,
                    mockedRoomFactory.Object,
                    mockedRoomsService.Object,
                    mockedCategoryFactory.Object,
                    mockedCategoriesService.Object);

            // Act
            productManagementController.AddProduct(productsOperationViewModel);

            // Assert
            mockedImageUploader.Verify(iu => iu.Upload(It.IsAny<HttpPostedFileBase>(),
                It.IsAny<HttpServerUtilityBase>(),
                imageFileName,
                folderName), Times.Once);
        }
    }
}
