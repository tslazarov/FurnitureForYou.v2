using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.RoomsServiceTests
{
    [TestFixture]
    public class AddRoom
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullRoomIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var roomsService = new RoomsService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => roomsService.AddRoom(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullRoomIsPassed()
        {
            // Arrange
            var expectedExMessage = "Room cannot be null.";

            var mockedData = new Mock<IFFYData>();

            var roomsService = new RoomsService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                roomsService.AddRoom(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallAddMethodOfDataRoomRepository()
        {
            // Arrange
            var mockedRoom = new Mock<Room>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.RoomsRepository.Add(It.IsAny<Room>()))
                .Verifiable();

            var roomsService = new RoomsService(mockedData.Object);

            // Act
            roomsService.AddRoom(mockedRoom.Object);

            // Assert
            mockedData.Verify(d =>
                d.RoomsRepository.Add(mockedRoom.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedRoom = new Mock<Room>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.RoomsRepository.Add(It.IsAny<Room>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var roomsService = new RoomsService(mockedData.Object);

            // Act
            roomsService.AddRoom(mockedRoom.Object);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
