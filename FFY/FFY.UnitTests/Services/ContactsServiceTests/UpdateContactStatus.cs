using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Services.ContactsServiceTests
{
    [TestFixture]
    public class UpdateContactStatus
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContactIsPassed()
        {
            // Arrange
            var userId = "42";

            var mockedUser = new Mock<User>();
            var mockedData = new Mock<IFFYData>();

            var contactsService = new ContactsService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                contactsService.UpdateContactStatus(null,
                    mockedUser.Object,
                    ContactStatusType.NotProcessed,
                    userId));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContactIsPassed()
        {
            // Arrange
            var userId = "42";
            var expectedExMessage = "Contact cannot be null.";

            var mockedUser = new Mock<User>();
            var mockedData = new Mock<IFFYData>();

            var contactsService = new ContactsService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                contactsService.UpdateContactStatus(null,
                    mockedUser.Object,
                    ContactStatusType.NotProcessed,
                    userId));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserIsPassed()
        {
            // Arrange
            var userId = "42";

            var mockedContact = new Mock<Contact>();
            var mockedData = new Mock<IFFYData>();

            var contactsService = new ContactsService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                contactsService.UpdateContactStatus(mockedContact.Object,
                    null,
                    ContactStatusType.NotProcessed,
                    userId));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserIsPassed()
        {
            // Arrange
            var userId = "42";
            var expectedExMessage = "User cannot be null.";

            var mockedContact = new Mock<Contact>();
            var mockedData = new Mock<IFFYData>();

            var contactsService = new ContactsService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                contactsService.UpdateContactStatus(mockedContact.Object,
                    null,
                    ContactStatusType.NotProcessed,
                    userId));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldAssignNewStatusOnContact()
        {
            // Arrange
            var userId = "42";
            var status = ContactStatusType.Processing;

            var contact = new Contact() { ContactStatusType = ContactStatusType.NotProcessed };
            var mockedUser = new Mock<User>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.Update(It.IsAny<Contact>()));
            mockedData.Setup(d => d.SaveChanges());

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            contactsService.UpdateContactStatus(contact, mockedUser.Object, status, userId);

            // Assert
            Assert.AreEqual(status, contact.ContactStatusType);
        }

        [Test]
        public void ShouldAssignUserProcessedByAndId_WhenContactStatusTypeIsProcessingOrProcessed()
        {
            // Arrange
            var userId = "42";
            var status = ContactStatusType.Processing;

            var contact = new Contact() { ContactStatusType = ContactStatusType.NotProcessed };
            var mockedUser = new Mock<User>();

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.Update(It.IsAny<Contact>()));
            mockedData.Setup(d => d.SaveChanges());

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            contactsService.UpdateContactStatus(contact, mockedUser.Object, status, userId);

            // Assert
            Assert.AreEqual(mockedUser.Object, contact.UserProcessedBy);
            Assert.AreEqual(userId, contact.UserProccessedById);
        }

        [Test]
        public void ShouldChangeUserProccesedByAndIdToNull_WhenContactStatusTypeIsNotProcessed()
        {
            // Arrange
            var userId = "42";
            var status = ContactStatusType.NotProcessed;

            var contact = new Contact() { ContactStatusType = ContactStatusType.NotProcessed };
            var mockedUser = new Mock<User>();

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.Update(It.IsAny<Contact>()));
            mockedData.Setup(d => d.SaveChanges());

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            contactsService.UpdateContactStatus(contact, mockedUser.Object, status, userId);

            // Assert
            Assert.AreEqual(null, contact.UserProcessedBy);
            Assert.AreEqual(null, contact.UserProccessedById);
        }

        [Test]
        public void ShouldCallUpdateMethodOfDataContactsRepository()
        {
            // Arrange
            var userId = "42";
            var status = ContactStatusType.Processing;

            var contact = new Contact() { ContactStatusType = ContactStatusType.NotProcessed };
            var mockedUser = new Mock<User>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.Update(It.IsAny<Contact>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges());

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            contactsService.UpdateContactStatus(contact, mockedUser.Object, status, userId);

            // Assert
            mockedData.Verify(d => d.ContactsRepository.Update(contact), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var userId = "42";
            var status = ContactStatusType.Processing;

            var contact = new Contact() { ContactStatusType = ContactStatusType.NotProcessed };
            var mockedUser = new Mock<User>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.Update(It.IsAny<Contact>()));
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            contactsService.UpdateContactStatus(contact, mockedUser.Object, status, userId);

            // Assert
            mockedData.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
