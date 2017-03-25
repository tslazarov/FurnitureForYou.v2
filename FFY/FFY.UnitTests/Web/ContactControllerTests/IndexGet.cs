using FFY.Data.Factories;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ContactControllerTests
{
    [TestFixture]
    public class IndexGet
    {
        [Test]
        public void ShouldReturnDefaultView()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedContactFactory = new Mock<IContactFactory>();
            var mockedContactsService = new Mock<IContactsService>();

            var contactController = new ContactController(mockedDateTimeProvider.Object,
                   mockedContactFactory.Object,
                   mockedContactsService.Object);

            // Act and Assert
            contactController.WithCallTo(cc => cc.Index())
                .ShouldRenderDefaultView();
        }
    }
}
