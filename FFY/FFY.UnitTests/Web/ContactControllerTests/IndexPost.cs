using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Controllers;
using FFY.Web.Models.Contact;
using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ContactControllerTests
{
    [TestFixture]
    public class IndexPost
    {
        [Test]
        public void ShouldCallGetCurrentTimeMethodOfDateTimeProvider()
        {
            // Arrange
            var contactViewModel = new ContactViewModel();

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime()).Verifiable();
            var mockedContactFactory = new Mock<IContactFactory>();
            mockedContactFactory.Setup(cf => cf.CreateContact(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<ContactStatusType>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.AddContact(It.IsAny<Contact>()));

            var contactController = new ContactController(mockedDateTimeProvider.Object,
                   mockedContactFactory.Object,
                   mockedContactsService.Object);

            // Act
            contactController.Index(contactViewModel);

            // Assert
            mockedDateTimeProvider.Verify(dtp => dtp.GetCurrentTime(), Times.Once);
        }

        [Test]
        public void ShouldSetSendOnOfContactViewModelToCurrentTime()
        {
            // Arrange
            var currentTime = new DateTime(2017, 3, 26);
            var contactViewModel = new ContactViewModel();

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Returns(currentTime);
            var mockedContactFactory = new Mock<IContactFactory>();
            mockedContactFactory.Setup(cf => cf.CreateContact(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<ContactStatusType>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.AddContact(It.IsAny<Contact>()));

            var contactController = new ContactController(mockedDateTimeProvider.Object,
                   mockedContactFactory.Object,
                   mockedContactsService.Object);

            // Act
            contactController.Index(contactViewModel);

            // Assert
            Assert.AreEqual(currentTime, contactViewModel.SendOn);
        }

        [Test]
        public void ShouldSetStatusTypeOfContactViewModelToNotProcessed()
        {
            // Arrange
            var contactViewModel = new ContactViewModel();

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime());
            var mockedContactFactory = new Mock<IContactFactory>();
            mockedContactFactory.Setup(cf => cf.CreateContact(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<ContactStatusType>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.AddContact(It.IsAny<Contact>()));

            var contactController = new ContactController(mockedDateTimeProvider.Object,
                   mockedContactFactory.Object,
                   mockedContactsService.Object);

            // Act
            contactController.Index(contactViewModel);

            // Assert
            Assert.AreEqual(ContactStatusType.NotProcessed, contactViewModel.StatusType);
        }

        [Test]
        public void ShouldCallCreateContactMethodOfContactFactory()
        {
            // Arrange
            var currentTime = new DateTime(2017, 3, 26);

            var contactViewModel = new ContactViewModel() {
                Title = "Spam",
                Email = "me@mail.com",
                Content = "Random content",
                StatusType = ContactStatusType.NotProcessed,
                SendOn = currentTime
            };

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Returns(currentTime);
            var mockedContactFactory = new Mock<IContactFactory>();
            mockedContactFactory.Setup(cf => cf.CreateContact(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<ContactStatusType>()))
                .Verifiable();
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.AddContact(It.IsAny<Contact>()));

            var contactController = new ContactController(mockedDateTimeProvider.Object,
                   mockedContactFactory.Object,
                   mockedContactsService.Object);

            // Act
            contactController.Index(contactViewModel);

            // Assert
            mockedContactFactory.Verify(cf => cf.CreateContact(contactViewModel.Title,
                contactViewModel.Email,
                contactViewModel.Content,
                contactViewModel.SendOn,
                It.IsAny<ContactStatusType>()), Times.Once);
        }

        [Test]
        public void ShouldCallAddContactMethodOfContactsService()
        {
            // Arrange
            var contact = new Contact();
            var contactViewModel = new ContactViewModel();

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime());
            var mockedContactFactory = new Mock<IContactFactory>();
            mockedContactFactory.Setup(cf => cf.CreateContact(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<ContactStatusType>()))
                .Returns(contact);
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.AddContact(It.IsAny<Contact>()))
                .Verifiable();

            var contactController = new ContactController(mockedDateTimeProvider.Object,
                   mockedContactFactory.Object,
                   mockedContactsService.Object);

            // Act
            contactController.Index(contactViewModel);

            // Assert
            mockedContactsService.Verify(cs => cs.AddContact(contact), Times.Once);
        }

        [Test]
        public void ShouldReturnDefaultView()
        {
            // Arrange
            var contactViewModel = new ContactViewModel();

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime());
            var mockedContactFactory = new Mock<IContactFactory>();
            mockedContactFactory.Setup(cf => cf.CreateContact(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<ContactStatusType>()));
            var mockedContactsService = new Mock<IContactsService>();
            mockedContactsService.Setup(cs => cs.AddContact(It.IsAny<Contact>()));

            var contactController = new ContactController(mockedDateTimeProvider.Object,
                   mockedContactFactory.Object,
                   mockedContactsService.Object);

            // Act and Assert
            contactController.WithCallTo(cc => cc.Index(contactViewModel))
                .ShouldRenderDefaultView();
        }
    }
}
