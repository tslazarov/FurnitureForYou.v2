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
    public class GetContactById
    {
        [TestCase(1, "elon@tesla.com")]
        [TestCase(5, "elon@spacex.com")]
        public void ShouldCallGetByIdMethodOfDataContactsRepository(int id, string email)
        {
            // Arrange

            var contact = new Contact() { Id = id, Email = email };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.GetById(It.IsAny<int>()))
                .Returns(contact)
                .Verifiable();

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            contactsService.GetContactById(id);

            // Assert
            mockedData.Verify(d => d.ContactsRepository.GetById(id), Times.Once);
        }

        [TestCase(1, "elon@tesla.com")]
        [TestCase(5, "elon@spacex.com")]
        public void ShouldReturnCorrectCategory(int id, string email)
        {
            // Arrange
            var contact = new Contact() { Id = id, Email = email };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.GetById(It.IsAny<int>()))
                .Returns(contact)
                .Verifiable();

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.GetContactById(id);

            // Assert
            Assert.AreEqual(contact, result);
        }
    }
}
