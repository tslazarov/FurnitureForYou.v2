using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models;
using FFY.Web.Areas.Administration.Models.ContactManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ContactManagementControllerTests
{
    [TestFixture]
    public class SearchContacts
    {
        [Test]
        public void ShouldCallSearchContactsMethodOfContactsService()
        {
            // Arrange
            var page = 1;
            var contactsPerPage = 10;

            var searchModel = new SearchModel();
            var contactsViewModel = new ContactsViewModel();

            var contact = new Contact();
            var contacts = new List<Contact>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleContactViewModel>>(It.IsAny<object>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.SearchContacts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(contacts)
                .Verifiable();
            mockedContactsService.Setup(cs => cs.GetContactsCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(contacts.Count);
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.SearchContacts(searchModel, contactsViewModel, page);

            // Assert
            mockedContactsService.Verify(cs => 
                cs.SearchContacts(searchModel.SearchWord,
                    searchModel.SortBy,
                    searchModel.FilterBy,
                    page,
                    contactsPerPage), Times.Once);
        }

        [Test]
        public void ShouldCallGetContactsCountMethodOfContactsService()
        {
            // Arrange
            var searchModel = new SearchModel();
            var contactsViewModel = new ContactsViewModel();

            var contact = new Contact();
            var contacts = new List<Contact>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleContactViewModel>>(It.IsAny<object>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.SearchContacts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(contacts);
            mockedContactsService.Setup(cs => cs.GetContactsCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(contacts.Count)
                .Verifiable();
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.SearchContacts(searchModel, contactsViewModel, null);

            // Assert
            mockedContactsService.Verify(cs =>
                cs.GetContactsCount(searchModel.SearchWord, searchModel.FilterBy), Times.Once);
        }

        [Test]
        public void ShouldSetSearchModelOfContactsViewModel()
        {
            // Arrange
            var page = 1;

            var searchModel = new SearchModel();
            var contactsViewModel = new ContactsViewModel();

            var contact = new Contact();
            var contacts = new List<Contact>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleContactViewModel>>(It.IsAny<object>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.SearchContacts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(contacts);
            mockedContactsService.Setup(cs => cs.GetContactsCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(contacts.Count);
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.SearchContacts(searchModel, contactsViewModel, page);

            // Assert
            Assert.AreSame(searchModel, contactsViewModel.SearchModel);
        }

        [Test]
        public void ShouldSetContactsCountOfContactsViewModel()
        {
            // Arrange
            var page = 1;

            var searchModel = new SearchModel();
            var contactsViewModel = new ContactsViewModel();

            var contact = new Contact();
            var contacts = new List<Contact>() { contact };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleContactViewModel>>(It.IsAny<object>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.SearchContacts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(contacts);
            mockedContactsService.Setup(cs => cs.GetContactsCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(contacts.Count);
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.SearchContacts(searchModel, contactsViewModel, page);

            // Assert
            Assert.AreEqual(contacts.Count, contactsViewModel.ContactsCount);
        }

        [Test]
        public void ShouldSetPagesOfContactsViewModel()
        {
            // Arrange
            var contactsPerPage = 10;
            var page = 1;

            var searchModel = new SearchModel();
            var contactsViewModel = new ContactsViewModel();

            var contact = new Contact();
            var contacts = new List<Contact>() { contact };

            var expectedPages = (int)Math.Ceiling((double)contacts.Count / contactsPerPage);

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleContactViewModel>>(It.IsAny<object>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.SearchContacts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(contacts);
            mockedContactsService.Setup(cs => cs.GetContactsCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(contacts.Count);
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.SearchContacts(searchModel, contactsViewModel, page);

            // Assert
            Assert.AreEqual(expectedPages, contactsViewModel.Pages);
        }

        [Test]
        public void ShouldSetPageOfContactsViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var contactsViewModel = new ContactsViewModel();

            var contact = new Contact();
            var contacts = new List<Contact>() { contact };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleContactViewModel>>(It.IsAny<object>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.SearchContacts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(contacts);
            mockedContactsService.Setup(cs => cs.GetContactsCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(contacts.Count);
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.SearchContacts(searchModel, contactsViewModel, page);

            // Assert
            Assert.AreEqual(page, contactsViewModel.Page);
        }

        [Test]
        public void ShouldMapAndSetContactsOfContactsViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var contactsViewModel = new ContactsViewModel();

            var contact = new Contact();
            var contacts = new List<Contact>() { contact };

            var singleContacts = new List<SingleContactViewModel>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => 
                mp.Map<IEnumerable<SingleContactViewModel>>(It.IsAny<object>()))
                .Returns(singleContacts);
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.SearchContacts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(contacts);
            mockedContactsService.Setup(cs => cs.GetContactsCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(contacts.Count);
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act 
            contactManagementController.SearchContacts(searchModel, contactsViewModel, page);

            // Assert
            CollectionAssert.AreEquivalent(singleContacts, contactsViewModel.Contacts);
        }

        [Test]
        public void ShouldRenderContactsPartialViewWithContactsViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var contactsViewModel = new ContactsViewModel();

            var contact = new Contact();
            var contacts = new List<Contact>() { contact };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleContactViewModel>>(It.IsAny<object>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.SearchContacts(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(contacts);
            mockedContactsService.Setup(cs => cs.GetContactsCount(It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(contacts.Count);
            var mockedUsersService = new Mock<IUsersService>();

            var contactManagementController = new ContactManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedContactsService.Object,
                   mockedUsersService.Object);

            // Act and Assert
            contactManagementController.WithCallTo(cmc => cmc.SearchContacts(searchModel,
                contactsViewModel,
                page))
                .ShouldRenderPartialView("ContactsPartial")
                .WithModel<ContactsViewModel>(model => Assert.AreEqual(contactsViewModel, model));
        }
    }
}
