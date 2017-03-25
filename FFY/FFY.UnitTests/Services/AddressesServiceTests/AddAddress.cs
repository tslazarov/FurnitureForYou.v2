using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.AddressesServiceTests
{
    [TestFixture]
    public class AddAddress
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAddressIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var addressesService = new AddressesService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => addressesService.AddAddress(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAddressIsPassed()
        {
            // Arrange
            var expectedExMessage = "Address cannot be null.";

            var mockedData = new Mock<IFFYData>();

            var addressesService = new AddressesService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                addressesService.AddAddress(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallAddMethodOfDataAddressRepository()
        {
            // Arrange
            var mockedAddress = new Mock<Address>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.AddressesRepository.Add(It.IsAny<Address>()))
                .Verifiable();

            var addressesService = new AddressesService(mockedData.Object);

            // Act
            addressesService.AddAddress(mockedAddress.Object);

            // Assert
            mockedData.Verify(d =>
                d.AddressesRepository.Add(mockedAddress.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedAddress = new Mock<Address>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.AddressesRepository.Add(It.IsAny<Address>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var addressesService = new AddressesService(mockedData.Object);

            // Act
            addressesService.AddAddress(mockedAddress.Object);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
