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
    public class SearchContacts
    {
        [Test]
        public void ShouldReturnAllContacts_WhenNoSearchWordAndFilterAreProvided()
        {
            // Arrange
            var contacts = new List<Contact>()
            {
                new Contact() { Title = "Hello" },
                new Contact() { Title = "Important" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.SearchContacts(null, "title", null, 1, 10);

            // Assert
            CollectionAssert.AreEquivalent(contacts, result);
        }

        [Test]
        public void ShouldReturnCorrectContact_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "hello";

            var contacts = new List<Contact>()
            {
                new Contact() { Title = "Hello", Email="hello@mail.com" },
                new Contact() { Title = "Important", Email="important@mail.com" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.SearchContacts(searchWord, "title", "", 1, 10);

            // Assert
            Assert.AreSame(contacts[0], result.First());
        }

        [Test]
        public void ShouldReturnCorrectContactsCollection_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "mail";

            var contacts = new List<Contact>()
            {
                new Contact() { Title = "Hello", Email="hello@mail.com" },
                new Contact() { Title = "Important", Email="important@mail.com" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.SearchContacts(searchWord, "title", "", 1, 10);

            // Assert
            Assert.AreSame(contacts[0], result.First());
            Assert.AreSame(contacts[1], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectContactsSortedByTitle_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "mail";

            var contacts = new List<Contact>()
            {
                new Contact() { Title = "Hello", Email="hello@mail.com" },
                new Contact() { Title = "Important", Email="important@mail.com" },
                new Contact() { Title = "Click", Email="click@mail.com" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.SearchContacts(searchWord, "title", "",  1, 10);

            // Assert
            Assert.AreSame(contacts[2], result.First());
            Assert.AreSame(contacts[1], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectContactsSortedByEmail_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "mail";

            var contacts = new List<Contact>()
            {
                new Contact() { Title = "Hello", Email="hello@mail.com" },
                new Contact() { Title = "Zero", Email="alphabet@mail.com" },
                new Contact() { Title = "Click", Email="click@mail.com" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.SearchContacts(searchWord, "email", "", 1, 10);

            // Assert
            Assert.AreSame(contacts[1], result.First());
            Assert.AreSame(contacts[0], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectContactsSortedByDate_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "mail";

            var contacts = new List<Contact>()
            {
                new Contact() {
                    Title = "Hello",
                    Email ="hello@mail.com",
                    SendOn = new DateTime(2017, 1, 1)
                },
                new Contact()
                {
                    Title = "Zero",
                    Email ="alphabet@mail.com",
                    SendOn = new DateTime(2016, 12, 1)
                },
                new Contact() {
                    Title = "Click",
                    Email ="click@mail.com",
                    SendOn = new DateTime(2017, 3, 1)
                }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.SearchContacts(searchWord, "date", "", 1, 10);

            // Assert
            Assert.AreSame(contacts[2], result.First());
            Assert.AreSame(contacts[1], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectContactsPerPage_WhenSearchWordAndPageAreProvided()
        {
            // Arrange
            var searchWord = "mail";

            var contacts = new List<Contact>()
            {
                new Contact() { Title = "Hello", Email="hello@mail.com" },
                new Contact() { Title = "Zero", Email="alphabet@mail.com" },
                new Contact() { Title = "Click", Email="click@mail.com" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.SearchContacts(searchWord, "title", "", 3, 1);

            // Assert
            Assert.AreSame(contacts[1], result.First());
        }

        [Test]
        public void ShouldReturnNoContacts_WhenNoContactsAreMatchingTheCriterias()
        {
            // Arrange
            var searchWord = "qwerty";

            var contacts = new List<Contact>()
            {
                new Contact() { Title = "Hello", Email="hello@mail.com" },
                new Contact() { Title = "Zero", Email="alphabet@mail.com" },
                new Contact() { Title = "Click", Email="click@mail.com" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ContactsRepository.All())
                .Returns(contacts.AsQueryable);

            var contactsService = new ContactsService(mockedData.Object);

            // Act
            var result = contactsService.SearchContacts(searchWord, "title", "", 1, 10);

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
