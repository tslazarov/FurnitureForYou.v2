using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FFY.UnitTests.Services.ContactsServiceTests
{
    [TestFixture]
    public class GetContactsCount
    {
        [Test]
        public void ShouldReturnAllContactsCount_WhenNoSearchWordAndFilterAreProvided()
        {
            // Arrange
            var contacts = new List<Contact>()
            {
                new Contact() { Title = "Spam" },
                new Contact() { Title = "Offer" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.GetContactsCount(null, null);

            // Assert
            Assert.AreEqual(contacts.Count, result);
        }

        [TestCase("spam", 1)]
        [TestCase("mail", 3)]
        [TestCase("x", 0)]
        public void ShouldReturnCorrectContactsCount_WhenSearchWordIsProvided(string searchWord,
            int expectedCount)
        {
            // Arrange
            var contacts = new List<Contact>()
            {
                new Contact() { Title = "Spam", Email="spam@mail.com" },
                new Contact() { Title = "Offer", Email="offer@mail.com" },
                new Contact() { Title = "Random", Email="random@mail.com" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.GetContactsCount(searchWord, null);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestCase("1", 2)]
        [TestCase("0", 3)]
        [TestCase("3", 1)]
        public void ShouldReturnCorrectContactsCount_WhenFilterIsProvided(string filterBy,
            int expectedCount)
        {
            // Arrange
            var contacts = new List<Contact>()
            {
                new Contact() { ContactStatusType = ContactStatusType.NotProcessed },
                new Contact() { ContactStatusType = ContactStatusType.NotProcessed },
                new Contact() { ContactStatusType = ContactStatusType.Processed }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.GetContactsCount(null, filterBy);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestCase("mail", "1", 2)]
        [TestCase("mail", "0", 3)]
        [TestCase("spam", "2", 0)]
        [TestCase("offer", "1", 1)]
        public void ShouldReturnCorrectContactsCount_WhenSearchWordAndFilterAreProvided(string searchWord,
            string filterBy,
            int expectedCount)
        {
            // Arrange
            var contacts = new List<Contact>()
            {
                new Contact() {
                    Title = "Spam",
                    Email = "spam@mail.com",
                    ContactStatusType = ContactStatusType.NotProcessed
                },
                new Contact() {
                    Title = "Offer",
                    Email = "offer@mail.com",
                    ContactStatusType = ContactStatusType.NotProcessed
                },
                new Contact() {
                    Title = "Random",
                    Email = "random@mail.com",
                    ContactStatusType = ContactStatusType.Processed
                },
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.GetContactsCount(searchWord, filterBy);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }
    }
}
