using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models.ContactManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ContactManagementControllerTests
{
    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldReturnDefaultViewWithContactsViewModel()
        {
            // Arrange
            var contactsViewModel = new ContactsViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act and Assert
            contactManagementController.WithCallTo(cmc => cmc.Index(contactsViewModel))
                .ShouldRenderDefaultView()
                .WithModel<ContactsViewModel>(model => Assert.AreEqual(contactsViewModel, model));
        }
    }
}
