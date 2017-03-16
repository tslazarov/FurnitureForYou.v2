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
    public class AddContact
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContactIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var contactsService = new ContactsService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => contactsService.AddContact(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContactIsPassed()
        {
            // Arrange
            var expectedExMessage = "Contact cannot be null.";

            var mockedData = new Mock<IFFYData>();

            var contactsService = new ContactsService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                contactsService.AddContact(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallAddMethodOfDataContactRepository()
        {
            // Arrange
            var mockedContact = new Mock<Contact>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.Add(It.IsAny<Contact>()))
                .Verifiable();

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            contactsService.AddContact(mockedContact.Object);

            // Assert
            mockedData.Verify(d =>
                d.ContactsRepository.Add(mockedContact.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedContact = new Mock<Contact>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.Add(It.IsAny<Contact>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            contactsService.AddContact(mockedContact.Object);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
