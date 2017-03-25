using FFY.Models;
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
    public class ContactDetailed
    {

        [Test]
        public void ShouldCallGetContactByIdMethodOfContactsService()
        {
            // Arrange
            var id = 10;
            var contactViewModel = new ContactViewModel();
            var contact = new Contact();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.GetContactById(It.IsAny<int>()))
                .Verifiable();
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.ContactDetailed(contactViewModel, id);

            // Assert
            mockedContactsService.Verify(cs => cs.GetContactById(id), Times.Once);
        }

        [Test]
        public void ShouldAssignContactToContactViewModel_WhenContactIsFound()
        {
            // Arrange
            var id = 10;
            var contactViewModel = new ContactViewModel();
            var contact = new Contact();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.GetContactById(It.IsAny<int>()))
                .Returns(contact);
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act
            contactManagementController.ContactDetailed(contactViewModel, id);

            // Assert
            Assert.AreSame(contact, contactViewModel.Contact);
        }

        [Test]
        public void ShouldReturnPageNotFoundView_WhenContactIsNotFound()
        {
            // Arrange
            var id = 10;
            var contactViewModel = new ContactViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act and Assert
            contactManagementController.WithCallTo(cmc => cmc.ContactDetailed(contactViewModel, id))
                .ShouldRenderView("PageNotFound");
        }

        [Test]
        public void ShouldReturnDefaultViewWithContactViewModel_WhenContactIsFound()
        {
            // Arrange
            var id = 10;
            var contactViewModel = new ContactViewModel();
            var contact = new Contact();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.GetContactById(It.IsAny<int>()))
                .Returns(contact);
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act and Assert
            contactManagementController.WithCallTo(cmc => cmc.ContactDetailed(contactViewModel, id))
                .ShouldRenderDefaultView()
                .WithModel<ContactViewModel>(model => Assert.AreEqual(contactViewModel, model));
        }
    }
}
