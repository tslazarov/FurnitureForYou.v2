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
    public class UpdateStatus
    {
        [Test]
        public void ShouldReadCurrentUserIdOfAuthenticationProvider()
        {
            // Arrange
            var id = "42";
            var contactViewModel = new ContactViewModel()
            {
                Contact = new Contact() { Id = 13 }
            };
            var contact = new Contact();
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id)
                .Verifiable();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.GetContactById(It.IsAny<int>()))
                .Returns(contact);
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.UpdateStatus(contactViewModel);

            // Assert
            mockedAuthenticationProvider.VerifyGet(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void ShouldCallGetContactByIdMethodOfContactsService()
        {
            // Arrange
            var id = "42";
            var contactId = 2;
            var contactViewModel = new ContactViewModel() {
                Contact = new Contact() { Id = contactId }
            };
            var contact = new Contact();
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.GetContactById(It.IsAny<int>()))
                .Returns(contact)
                .Verifiable();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.UpdateStatus(contactViewModel);

            // Assert
            mockedContactsService.Verify(cs => cs.GetContactById(contactId), Times.Once);
        }

        [Test]
        public void ShouldCallGetUserByIdMethodOfUsersService()
        {
            // Arrange
            var id = "42";
            var contactViewModel = new ContactViewModel()
            {
                Contact = new Contact() { Id = 13 }
            };
            var contact = new Contact();
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.GetContactById(It.IsAny<int>()))
                .Returns(contact);
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();
            
            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.UpdateStatus(contactViewModel);

            // Assert
            mockedUsersService.Verify(cs => cs.GetUserById(id), Times.Once);
        }

        [Test]
        public void ShouldCallUpdateContactStatusMethodOfContactsService()
        {
            // Arrange
            var id = "42";
            var contactViewModel = new ContactViewModel()
            {
                Contact = new Contact() {
                    Id = 13,
                    ContactStatusType = ContactStatusType.Processed
                }
            };
            var contact = new Contact();
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.GetContactById(It.IsAny<int>()))
                .Returns(contact);
            mockedContactsService.Setup(cs => cs.UpdateContactStatus(It.IsAny<Contact>(),
                It.IsAny<User>(),
                It.IsAny<ContactStatusType>(),
                It.IsAny<string>()))
                .Verifiable();
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user);

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.UpdateStatus(contactViewModel);

            // Assert
            mockedContactsService.Verify(cs => 
                cs.UpdateContactStatus(contact, user, It.IsAny<ContactStatusType>(), id), Times.Once);
        }

        [Test]
        public void ShouldRedirectToContactDetailed()
        {
            // Arrange
            var id = "42";
            var contactViewModel = new ContactViewModel()
            {
                Contact = new Contact() { Id = 13 }
            };
            var contact = new Contact();
            var user = new User();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            mockedAuthenticationProvider.SetupGet(ap => ap.CurrentUserId)
                .Returns(id);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.GetContactById(It.IsAny<int>()))
                .Returns(contact);
            var mockedUsersService = new Mock<IUsersService>();
            mockedUsersService.Setup(us => us.GetUserById(It.IsAny<string>()))
                .Returns(user)
                .Verifiable();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act and Assert
            contactManagementController.WithCallTo(cmc => cmc.UpdateStatus(contactViewModel))
                .ShouldRedirectTo((ContactManagementController cm) => 
                    cm.ContactDetailed(contactViewModel, contactViewModel.Contact.Id));
        }
    }
}
